using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Storefront.API.Data.Base
{
    public class RepositoryBase<TModel, TKey> : IDisposable
        where TModel : class 
        where TKey : struct, IEquatable<TKey>
    {
        protected readonly DbSet<TModel> _dbSet;
        private readonly StorefrontContext _context;
        private bool disposed = false;
        public RepositoryBase(StorefrontContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<TModel>();
        }
        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<TModel>> Get(
            Expression<Func<TModel, bool>> filter = null!,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null!,
            List<string> includedProperties = null!)
        {
            IQueryable<TModel> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);    
            }

            if (includedProperties != null)
            {
                foreach (string property in includedProperties)
                {
                    query = query.Include(property);
                }
            }

            if(orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }
        public virtual async Task<TModel?> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task Insert(TModel model)
        {
            if (_dbSet.Contains(model))
            {
                throw new ArgumentException($"Tried to insert duplicate entity of type {typeof(TModel)}.");
            }
            await _dbSet.AddAsync(model);
        }
        public void Update(TModel entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TKey id)
        {
            TModel? entityToDelete = _dbSet.Find(id);

            if(entityToDelete is null)
            {
                throw new ArgumentOutOfRangeException($"Unable to find entity of type {typeof(TModel)} with ID {id}.");
            }
                
            Delete(entityToDelete);
        }

        public virtual void Delete(TModel entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

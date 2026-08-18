using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Storefront.API.Data.Base
{
    public class RepositoryBase<TModel, TKey, TContext> : IDisposable
        where TModel : class 
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
        where TContext : DbContext
    {
        protected readonly DbSet<TModel> _dbSet;
        private Serilog.ILogger _logger;
        private readonly TContext _context;
        private bool disposed = false;
        public RepositoryBase(TContext dbContext, Serilog.ILogger logger)
        {
            _context = dbContext;
            _dbSet = _context.Set<TModel>();
            _logger = logger;
        }
        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public IEnumerable<TModel> GetPage(TKey lastId, int pageSize)
        {
            return _dbSet.OrderBy(m => GetObjectKey(m))
                .Where(m => GetObjectKey(m).CompareTo(lastId) > 0)
                .Take(pageSize)
                .ToList();
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
        public async Task<TModel> Save(TModel entity)
        {
            if (!_dbSet.Contains(entity))
            {
                await Insert(entity);
            }
            else
            {
                Update(entity);
            }

            await SaveChanges();

            TKey keyValue = GetObjectKey(entity);
            TModel? savedEntity = await GetById(keyValue);
            if(savedEntity is null)
            {
                throw new KeyNotFoundException($"Unable to find {nameof(TModel)} with primary key {keyValue}.");
            }

            return savedEntity;
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
        private TKey GetObjectKey(TModel model)
        {
            var entry = _context.Entry(model);
            if(entry is null)
            {
                throw new ArgumentOutOfRangeException($"Unable to find {nameof(TModel)}");
            }

            List<IProperty>? primaryKeyProperties = entry.Metadata.FindPrimaryKey()?
                .Properties
                .ToList();


            if(primaryKeyProperties is null || (!primaryKeyProperties?.Any() ?? false))
            {
                throw new NotSupportedException($"No primary keys specified for {nameof(TModel)}. Unable to save.");
            }
            else if(primaryKeyProperties?.Count > 1)
            {
                throw new NotSupportedException($"Multiple primary keys specified for {nameof(TModel)}. Unable to save.");
            }

            IProperty primaryKeyProp = primaryKeyProperties!.FirstOrDefault()!;
            object? primaryKey = entry.Property(primaryKeyProp.Name).CurrentValue;

            if(primaryKey is null)
            {
                throw new ArgumentException($"No value for primary key {primaryKeyProp.Name}");
            }
            return (TKey)primaryKey;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
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

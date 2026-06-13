using Storefront.API.Data.Base;
using Storefront.API.Data.Repositories;

namespace Storefront.API.Data
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private StorefrontContext _context = null!;
        private ApplicationUserRepository _userRepository = null!;
        private ApplicationRoleRepository _roleRepository = null!;
        public UnitOfWork(StorefrontContext context)
        {
            _context = context;
        }

        public ApplicationUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new ApplicationUserRepository(_context);
                }
                return _userRepository;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public ApplicationRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new ApplicationRoleRepository(_context);
                }
                return _roleRepository;
            }
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

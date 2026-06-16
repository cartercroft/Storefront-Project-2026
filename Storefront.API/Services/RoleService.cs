using Microsoft.AspNetCore.Identity;
using Storefront.API.Data;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Services.Base;

namespace Storefront.API.Services
{
    public class RoleService : ServiceBase<ApplicationRole, Guid>, IRoleStore<ApplicationRole>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ApplicationRoleRepository _repository;
        public RoleService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.RoleRepository;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Insert(role);
            await _unitOfWork.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _repository.Delete(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<ApplicationRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(roleId, out Guid id))
            {
                throw new ArgumentException($"Could not parse {roleId} as {typeof(Guid)}");
            }
            return await _repository.GetById(id);
        }

        public async Task<ApplicationRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.Get(r => r.NormalizedName == normalizedRoleName)).FirstOrDefault();
        }

        public Task<string?> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string?> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(ApplicationRole role, string? roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _repository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}

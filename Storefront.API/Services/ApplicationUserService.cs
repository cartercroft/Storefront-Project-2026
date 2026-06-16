using Storefront.API.Classes;
using Storefront.API.Data;
using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services.Base;

namespace Storefront.API.Services
{
    public class ApplicationUserService : ServiceBase<ApplicationUser, Guid>
    {
        private readonly UnitOfWork _unitOfWork;
        public ApplicationUserService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<IEnumerable<ApplicationUserModel>>> GetAll()
        {
            Response<IEnumerable<ApplicationUserModel>> response = new Response<IEnumerable<ApplicationUserModel>>();

            //TODO: Fix with automapper
            List<ApplicationUserModel> users = new List<ApplicationUserModel>();

            foreach(ApplicationUser user in await _unitOfWork.UserRepository.GetAll())
            {
                ApplicationUserModel userModel = new ApplicationUserModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Email = user.Email
                };
                users.Add(userModel);
            }

            response.Result = users;
            
            return response;
        }
    }
}

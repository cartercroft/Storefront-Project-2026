using AutoMapper;
using Storefront.API.Data.Models;
using Storefront.API.Models;

namespace Storefront.API.Classes
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ReverseMap();
            CreateMap<ApplicationRole, ApplicationRoleViewModel>()
                .ReverseMap();
            CreateMap<Item, ItemViewModel>()
                .ReverseMap();
        }
    }
}

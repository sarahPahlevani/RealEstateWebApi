using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.RBAC
{
    public class UserAccountWishListController : ModelController<UserAccountWishList, UserAccountWishListDto>
    {
        public UserAccountWishListController(IModelService<UserAccountWishList, UserAccountWishListDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<UserAccountWishList>, IQueryable<UserAccountWishListDto>> DtoConverter
            => items => items.Select(i => new UserAccountWishListDto
            {
                Id = i.Id,
                PropertyId = i.PropertyId,
                UserAccountId = i.UserAccountId
            });
    }
}

using System;
using System.Linq;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.SharingSystem;

namespace RealEstateAgency.Controllers.SharingSystem
{
    public class SharedPropertyController : ModelController<SharedProperty, SharedPropertyDto>
    {
        public SharedPropertyController(IModelService<SharedProperty, SharedPropertyDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<SharedProperty>, IQueryable<SharedPropertyDto>> DtoConverter =>
            items => items.Select(i => new SharedPropertyDto
            {
                Id = i.Id,
                PropertyId = i.PropertyId,
                UserAccountId = i.UserAccountId,
                SocialNetworkId = i.SocialNetworkId,
                ReferralCodeAndSharingNetworkId = i.ReferralCodeAndSharingNetworkId
            });
    }
}

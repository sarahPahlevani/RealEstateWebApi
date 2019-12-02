using System;
using System.Linq;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.SharingSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using System.Threading;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Controllers.SharingSystem
{
    [AllowAnonymous]
    public class SharedPropertyController : ModelController<SharedProperty, SharedPropertyDto>
    {

        private readonly IPathProvider _pathProvider;

        public SharedPropertyController(IModelService<SharedProperty, SharedPropertyDto> modelService, IPathProvider pathProvider) : base(modelService)
        {
            _pathProvider = pathProvider;
        }

        public override Func<IQueryable<SharedProperty>, IQueryable<SharedPropertyDto>> DtoConverter =>
            items => items.Select(i => new SharedPropertyDto
            {
                Id = i.Id,
                PropertyId = i.PropertyId,
                UserAccountId = i.UserAccountId,
                SocialNetworkId = i.SocialNetworkId,
                RefererUrl = i.RefererUrl,
                ClickCount = i.ClickCount,
                ReferralCodeAndSharingNetworkId = i.ReferralCodeAndSharingNetworkId
            });

        [HttpGet("GetUserSharedProperty")]
        public async Task<ActionResult<PageResultDto<UserSharedPropertyDto>>>
            GetUserSharedPropertyAsync([FromQuery] int userId, [FromQuery] PageRequestDto requestDto, CancellationToken cancellationToken)
        {
            var list = await new PageResultDto<UserSharedPropertyDto>(
                    ModelService.Queryable.Where(r => r.UserAccountId == userId)
                        .Select(p => new UserSharedPropertyDto
                        {
                            Id = p.Id,
                            PropertyId = p.PropertyId,
                            PropertyType = p.Property.PropertyType.Name,
                            PropertyTitle = p.Property.Title,
                            PropertyPrice = p.Property.PropertyPrice.Price,
                            PropertyImageId = p.Property.PropertyImage.OrderBy(r => r.Priority).Select(r => new PropertyWebAppImageDto { Id = r.Id }).FirstOrDefault(),
                            UserAccountId = p.UserAccountId,
                            SocialNetworkId = p.SocialNetworkId,
                            SocialNetworkTitle = p.SocialNetwork.Name,
                            SocialNetworkIcon = p.SocialNetwork.LogoPicture,
                            RefererUrl = p.RefererUrl,
                            ClickCount = p.ClickCount,
                        }), requestDto)
                .GetPage(cancellationToken);

            foreach (var item in list.Items)
            {
                item.PropertyImageUrl = _pathProvider.GetImageApiPath<PropertyImage>(
                            nameof(PropertyImage.ImageContentTumblr), item.PropertyImageId.Id.ToString());
            }

            return list;
        }
    }
}

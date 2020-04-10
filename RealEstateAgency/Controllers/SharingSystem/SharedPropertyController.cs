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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public async Task<ActionResult<List<UserSharedPropertyDto>>>
            GetUserSharedPropertyAsync([FromQuery] int userId, [FromQuery] PageRequestDto requestDto, CancellationToken cancellationToken)
        {
            var query = await new PageResultDto<SharedProperty>(
                                ModelService.Queryable
                                .Include(r => r.Property)
                                .Include(r => r.Property.PropertyType)
                                .Include(r => r.Property.PropertyPrice)
                                .Include(r => r.Property.PropertyImage)
                                .Include(r => r.SocialNetwork)
                                .Where(r => r.UserAccountId == userId), requestDto)
                            .GetPage(cancellationToken);

            var list = query.Items
                .GroupBy(r => new { r.PropertyId, r.SocialNetworkId })
                .Select(p => new UserSharedPropertyDto
                {
                    PropertyId = p.First().PropertyId,
                    PropertyType = p.First().Property.PropertyType.Name,
                    PropertyTitle = p.First().Property.Title,
                    PropertyPrice = p.First().Property.PropertyPrice.Price,
                    PropertyImage = p.First().Property.PropertyImage.OrderBy(r => r.Priority).Select(r => new PropertyWebAppImageDto { Id = r.Id, ImagePath = r.ImagePath, TumbPath = r.TumbPath }).FirstOrDefault(),
                    UserAccountId = p.First().UserAccountId,
                    SocialNetworkId = p.First().SocialNetworkId,
                    SocialNetworkTitle = p.First().SocialNetwork?.Name,
                    SocialNetworkIcon = p.First().SocialNetwork?.Icon,
                    RefererUrl = p.First().RefererUrl,
                    ClickCount = p.Sum(r => r.ClickCount),
                }).ToList();

            return list;
        }
    }
}

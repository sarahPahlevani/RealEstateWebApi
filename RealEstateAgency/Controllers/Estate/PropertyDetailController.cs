using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using RealEstateAgency.Implementations.ApiImplementations.Models;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyDetailController : ModelController<PropertyDetail, PropertyDetailDto>
    {
        private readonly IEntityService<Property> _propertyService;

        public PropertyDetailController(IModelService<PropertyDetail, PropertyDetailDto> modelService
            , IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
        }

        public override Func<IQueryable<PropertyDetail>, IQueryable<PropertyDetailDto>> DtoConverter
        => items => items.Select(i => new PropertyDetailDto
        {
            Id = i.Id,
            Size = i.Size,
            LandArea = i.LandArea,
            Rooms = i.Rooms,
            Bedrooms = i.Bedrooms,
            Bathrooms = i.Bathrooms,
            Garages = i.Garages,
            GaragesSize = i.GaragesSize,
            YearBuild = i.YearBuild,
        });

        [HttpPost]
        public override async Task<ActionResult<PropertyDetailDto>> Create(PropertyDetailDto value, CancellationToken cancellationToken)
        {
            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            var property = await _propertyService.GetAsync(value.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return res;
        }

        [HttpPut]
        public override async Task<ActionResult> UpdateAsync(PropertyDetailDto value, CancellationToken cancellationToken)
        {
            await ModelService.UpdateByDtoAsync(value, cancellationToken);

            var property = await _propertyService.GetAsync(value.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("[Action]")]
        public ActionResult<NumberMinMax> GetMinMax()
        {
            var query = ModelService.Queryable;
            return new NumberMinMax
            {
                RoomMin = query.Where(r => r.IdNavigation.IsPublished).Min(r => r.Rooms).GetValueOrDefault(0),
                RoomMax = query.Where(r => r.IdNavigation.IsPublished).Max(r => r.Rooms).GetValueOrDefault(0),
                BedMin = query.Where(r => r.IdNavigation.IsPublished).Min(r => r.Bedrooms).GetValueOrDefault(0),
                BedMax = query.Where(r => r.IdNavigation.IsPublished).Max(r => r.Bedrooms).GetValueOrDefault(0),
                BathMin = query.Where(r => r.IdNavigation.IsPublished).Min(r => r.Bathrooms).GetValueOrDefault(0),
                BathMax = query.Where(r => r.IdNavigation.IsPublished).Max(r => r.Bathrooms).GetValueOrDefault(0),
                SizeMin = query.Where(r => r.IdNavigation.IsPublished).Min(r => r.Size).GetValueOrDefault(0),
                SizeMax = query.Where(r => r.IdNavigation.IsPublished).Max(r => r.Size).GetValueOrDefault(0),
            };
        }

    }
}

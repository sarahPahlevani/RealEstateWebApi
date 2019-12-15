using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyLocationController : ModelController<PropertyLocation, PropertyLocationDto>
    {
        private readonly IEntityService<Property> _propertyService;

        public PropertyLocationController(IModelService<PropertyLocation, PropertyLocationDto> modelService
            , IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
        }

        public override Func<IQueryable<PropertyLocation>, IQueryable<PropertyLocationDto>> DtoConverter
        => items => items.Select(i => new PropertyLocationDto
        {
            Id = i.Id,
            PropertyId = i.Id,
            //CountryId = i.CountryId,
            //RegionId = i.RegionId,
            //CityId = i.CityId,
            Country = i.Country,
            Region = i.Region,
            City = i.City,
            ZipCode = i.ZipCode,
            AddressLine1 = i.AddressLine1,
            AddressLine2 = i.AddressLine1,
            GoogleMapsLatitude = i.GoogleMapsLatitude,
            GoogleMapsLongitude = i.GoogleMapsLongitude,
        });

        [HttpPost]
        public override async Task<ActionResult<PropertyLocationDto>> Create(PropertyLocationDto value, CancellationToken cancellationToken)
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
        public override async Task<ActionResult> UpdateAsync(PropertyLocationDto value, CancellationToken cancellationToken)
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

    }
}

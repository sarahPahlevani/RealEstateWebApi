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
    public class PropertyAroundImportantLandmarkController
        : ModelController<PropertyAroundImportantLandmark, PropertyAroundImportantLandmarkDto>
    {
        private readonly IEntityService<Property> _propertyService;

        public PropertyAroundImportantLandmarkController(IModelService<PropertyAroundImportantLandmark, PropertyAroundImportantLandmarkDto> modelService
            , IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
        }

        public override
            Func<IQueryable<PropertyAroundImportantLandmark>, IQueryable<PropertyAroundImportantLandmarkDto>>
            DtoConverter => items => items.Select(i => new PropertyAroundImportantLandmarkDto
            {
                Id = i.Id,
                PropertyId = i.PropertyId,
                Description = i.Description,
                DistanceKm = i.DistanceKm,
                DistanceMiles = i.DistanceMiles,
                ImportantPlaceTypeId = i.ImportantPlaceTypeId,
                WalkToMin = i.WalkToMin
            });

        [HttpPost]
        public override async Task<ActionResult<PropertyAroundImportantLandmarkDto>> Create(PropertyAroundImportantLandmarkDto value, CancellationToken cancellationToken)
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
        public override async Task<ActionResult> UpdateAsync(PropertyAroundImportantLandmarkDto value, CancellationToken cancellationToken)
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

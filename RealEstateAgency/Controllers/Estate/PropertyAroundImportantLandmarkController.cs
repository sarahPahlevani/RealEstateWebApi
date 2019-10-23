using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyAroundImportantLandmarkController
        : ModelController<PropertyAroundImportantLandmark, PropertyAroundImportantLandmarkDto>
    {
        public PropertyAroundImportantLandmarkController(IModelService<PropertyAroundImportantLandmark, PropertyAroundImportantLandmarkDto> modelService) : base(modelService)
        {
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
    }
}

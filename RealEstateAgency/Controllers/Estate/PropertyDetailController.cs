using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyDetailController : ModelController<PropertyDetail, PropertyDetailDto>
    {
        public PropertyDetailController(IModelService<PropertyDetail, PropertyDetailDto> modelService) : base(modelService)
        {
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
    }
}

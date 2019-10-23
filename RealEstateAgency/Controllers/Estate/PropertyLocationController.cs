using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyLocationController : ModelController<PropertyLocation, PropertyLocationDto>
    {
        public PropertyLocationController(IModelService<PropertyLocation, PropertyLocationDto> modelService) : base(modelService)
        { }

        public override Func<IQueryable<PropertyLocation>, IQueryable<PropertyLocationDto>> DtoConverter
        => items => items.Select(i => new PropertyLocationDto
        {
            Id = i.Id,
            PropertyId = i.Id,
            CountryId = i.CountryId,
            RegionId = i.RegionId,
            CityId = i.CityId,
            ZipCode = i.ZipCode,
            AddressLine1 = i.AddressLine1,
            AddressLine2 = i.AddressLine1,
            GoogleMapsLatitude = i.GoogleMapsLatitude,
            GoogleMapsLongitude = i.GoogleMapsLongitude,
        });
    }
}

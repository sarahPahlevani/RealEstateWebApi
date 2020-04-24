using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyLocationController : ModelController<PropertyLocation, PropertyLocationDto>
    {
        private readonly IEntityService<Property> _propertyService;
        private readonly IEntityService<Country> _countryService;
        private readonly IEntityService<Region> _regionService;
        private readonly IEntityService<City> _cityService;

        public PropertyLocationController(IModelService<PropertyLocation, PropertyLocationDto> modelService
            , IEntityService<Property> propertyService
            , IEntityService<Country> countryService
            , IEntityService<Region> regionService
            , IEntityService<City> cityService) : base(modelService)
        {
            _propertyService = propertyService;
            _countryService = countryService;
            _regionService = regionService;
            _cityService = cityService;
        }

        public override Func<IQueryable<PropertyLocation>, IQueryable<PropertyLocationDto>> DtoConverter
        => items => items
        .Include(r => r.CityNavigation)
        .Include(r => r.CityNavigation.Region)
        .Include(r => r.CityNavigation.Region.Country)
        .Select(i => new PropertyLocationDto
        {
            Id = i.Id,
            PropertyId = i.Id,
            CountryId = i.CityNavigation.Region.CountryId,
            RegionId = i.CityNavigation.RegionId,
            CityId = i.CityId,
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
            var countryItem = _countryService.AsQueryable(r => r.Name == value.Country).FirstOrDefault();
            if (countryItem == null)
                countryItem = _countryService.Create(new Country
                {
                    CurrencyId = 11,
                    Name = value.Country,
                });

            var regionItem = _regionService.AsQueryable(r => r.CountryId == countryItem.Id && r.Name == value.Region).FirstOrDefault();
            if (regionItem == null)
                regionItem = _regionService.Create(new Region
                {
                    CountryId = countryItem.Id,
                    Name = value.Region,
                });

            var cityItem = _cityService.AsQueryable(r => r.RegionId == regionItem.Id && r.Name == value.City).FirstOrDefault();
            if (cityItem == null)
                cityItem = _cityService.Create(new City
                {
                    RegionId = regionItem.Id,
                    Name = value.City,
                });
            value.CityId = cityItem.Id;

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
            var countryItem = _countryService.AsQueryable(r => r.Name == value.Country).FirstOrDefault();
            if (countryItem == null)
                countryItem = _countryService.Create(new Country
                {
                    CurrencyId = 11,
                    Name = value.Country,
                });

            var regionItem = _regionService.AsQueryable(r => r.CountryId == countryItem.Id && r.Name == value.Region).FirstOrDefault();
            if (regionItem == null)
                regionItem = _regionService.Create(new Region
                {
                    CountryId = countryItem.Id,
                    Name = value.Region,
                });

            var cityItem = _cityService.AsQueryable(r => r.RegionId == regionItem.Id && r.Name == value.City).FirstOrDefault();
            if (cityItem == null)
                cityItem = _cityService.Create(new City
                {
                    RegionId = regionItem.Id,
                    Name = value.City,
                });
            value.CityId = cityItem.Id;


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

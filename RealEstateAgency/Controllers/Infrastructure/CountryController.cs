using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAgency.Controllers.Infrastructure
{
    [AllowAnonymous]
    public class CountryController : ModelPagingController<Country, CountryDto, CountryDto>
    {
        private readonly IEntityService<Property> _propertyService;


        public CountryController(IModelService<Country, CountryDto> modelService
            , IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
        }

        public override Func<IQueryable<Country>, IQueryable<CountryDto>> DtoConverter => _converter;
        public override Func<IQueryable<Country>, IQueryable<CountryDto>> PagingConverter => _converter;

        private Func<IQueryable<Country>, IQueryable<CountryDto>> _converter =>
            entities => entities.Select(i =>
                new CountryDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    CurrencyId = i.CurrencyId,
                    Isolong = i.Isolong,
                    Isoshort = i.Isoshort,
                    Isocode = i.Isocode,
                    OfficialLongForm = i.OfficialLongForm,
                    OfficialShortForm = i.OfficialShortForm
                });


        [HttpGet("[Action]")]
        public async Task<IEnumerable<CountryDto>> GetUsed(CancellationToken cancellationToken)
        {
            var list = await _propertyService.Queryable
                .Include(r => r.PropertyLocation)
                .Include(r => r.PropertyLocation.CityNavigation)
                .Include(r => r.PropertyLocation.CityNavigation.Region)
                .Include(r => r.PropertyLocation.CityNavigation.Region.Country)
                .Where(r => r.IsPublished && r.PropertyLocation.CityId.HasValue)
                .GroupBy(r => r.PropertyLocation.CityNavigation.Region.CountryId)
                .Select(r => r.Key).ToListAsync(cancellationToken);

            return await ModelService.Queryable
                .Where(r => list.Contains(r.Id))
                .Select(r => new CountryDto
                {
                    Id = r.Id,
                    CurrencyId = r.CurrencyId,
                    Name = r.Name,
                    Isolong = r.Isolong,
                    Isoshort = r.Isoshort,
                    Isocode = r.Isocode,
                    OfficialLongForm = r.OfficialLongForm,
                    OfficialShortForm = r.OfficialShortForm,
                }).ToListAsync(cancellationToken);

        }


    }
}

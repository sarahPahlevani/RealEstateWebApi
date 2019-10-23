using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class CountryController : ModelPagingController<Country, CountryDto, CountryDto>
    {
        public CountryController(IModelService<Country, CountryDto> modelService) : base(modelService)
        {
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
    }
}

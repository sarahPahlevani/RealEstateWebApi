using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateAgency.Controllers.Infrastructure
{
    [AllowAnonymous]
    public class CountryTranslateController : ModelController<CountryTranslate, CountryTranslateDto>
    {
        public CountryTranslateController(IModelService<CountryTranslate, CountryTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<CountryTranslate>, IQueryable<CountryTranslateDto>> DtoConverter
        => items => items.Select(i => new CountryTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            CountryId = i.CountryId
        });
    }
}

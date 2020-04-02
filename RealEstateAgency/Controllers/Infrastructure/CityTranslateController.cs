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
    public class CityTranslateController : ModelController<CityTranslate, CityTranslateDto>
    {
        public CityTranslateController(IModelService<CityTranslate, CityTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<CityTranslate>, IQueryable<CityTranslateDto>> DtoConverter =>
            items => items.Select(i => new CityTranslateDto
            {
                Id = i.Id,
                Name = i.Name,
                LanguageId = i.LanguageId,
                CityId = i.CityId
            });
    }
}

using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class PriceScaleUnitTranslateController : ModelController<PriceScaleUnitTranslate, PriceScaleUnitTranslateDto>
    {
        public PriceScaleUnitTranslateController(IModelService<PriceScaleUnitTranslate, PriceScaleUnitTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<PriceScaleUnitTranslate>, IQueryable<PriceScaleUnitTranslateDto>> DtoConverter
        => items => items.Select(i => new PriceScaleUnitTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            PriceScaleUnitId = i.PriceScaleUnitId
        });
    }
}

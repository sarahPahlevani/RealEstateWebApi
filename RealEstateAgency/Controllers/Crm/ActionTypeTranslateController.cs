using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class ActionTypeTranslateController : ModelController<ActionTypeTranslate, ActionTypeTranslateDto>
    {
        public ActionTypeTranslateController(IModelService<ActionTypeTranslate, ActionTypeTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<ActionTypeTranslate>, IQueryable<ActionTypeTranslateDto>> DtoConverter
        => items => items.Select(i => new ActionTypeTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            ActionTypeId = i.ActionTypeId
        });
    }
}

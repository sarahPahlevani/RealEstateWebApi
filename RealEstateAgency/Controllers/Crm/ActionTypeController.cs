using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class ActionTypeController : ModelController<ActionType, ActionTypeDto>
    {
        public ActionTypeController(IModelService<ActionType, ActionTypeDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<ActionType>, IQueryable<ActionTypeDto>> DtoConverter =>
             items => items.Select(i => new ActionTypeDto
             {
                 Id = i.Id,
                 Name = i.Name
             });
    }
}

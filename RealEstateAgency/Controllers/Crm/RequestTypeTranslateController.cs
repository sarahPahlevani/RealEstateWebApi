using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestTypeTranslateController : ModelController<RequestTypeTranslate, RequestTypeTranslateDto>
    {
        public RequestTypeTranslateController(IModelService<RequestTypeTranslate, RequestTypeTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RequestTypeTranslate>, IQueryable<RequestTypeTranslateDto>> DtoConverter
        => items => items.Select(i => new RequestTypeTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            RequestTypeId = i.RequestTypeId
        });
    }
}

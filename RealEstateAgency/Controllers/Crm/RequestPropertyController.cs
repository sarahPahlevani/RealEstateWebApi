using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestPropertyController : ModelController<RequestProperty, RequestPropertyDto>
    {
        public RequestPropertyController(IModelService<RequestProperty, RequestPropertyDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RequestProperty>, IQueryable<RequestPropertyDto>> DtoConverter
            => items => items.Select(i => new RequestPropertyDto
            {
                Id = i.Id,
                PropertyId = i.PropertyId,
                Description = i.Description,
                RequestId = i.RequestId
            });
    }
}

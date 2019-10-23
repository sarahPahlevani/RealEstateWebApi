using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestTypeController : ModelController<RequestType, RequestTypeDto>
    {
        public RequestTypeController(IModelService<RequestType, RequestTypeDto> modelService, RealEstateDbContext context) : base(modelService)
        {
        }

        public override Func<IQueryable<RequestType>, IQueryable<RequestTypeDto>> DtoConverter
            => items => items.Select(i => new RequestTypeDto
            {
                Id = i.Id,
                Name = i.Name,
                CanAddProperty = i.CanAddProperty
            });
    }
}

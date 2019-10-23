using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class PropertyTypeTranslateController : ModelController<PropertyTypeTranslate, PropertyTypeTranslateDto>
        , IGetFromTenant<PropertyTypeTranslateDto>
    {
        
        public PropertyTypeTranslateController(
            IModelService<PropertyTypeTranslate, PropertyTypeTranslateDto> modelService)
            : base(modelService)
        {
        }

        public override Func<IQueryable<PropertyTypeTranslate>, IQueryable<PropertyTypeTranslateDto>> DtoConverter
        => entities => entities.Select(i => new PropertyTypeTranslateDto
        {
            Name = i.Name,
            Id = i.Id,
            LanguageId = i.LanguageId,
            PropertyTypeId = i.PropertyTypeId
        });

        [HttpGet("PropertyTypeTranslates/{tenantId}")]
        public async Task<ActionResult<IEnumerable<PropertyTypeTranslateDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken)
        {
            var res = await ModelService.DataConvertQuery(ModelService
                    .AsQueryable(i => i.PropertyTypeId == tenantId))
                .ToListAsync(cancellationToken);
            return res;
        }
    }
}

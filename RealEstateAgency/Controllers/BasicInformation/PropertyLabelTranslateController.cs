using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class PropertyLabelTranslateController : ModelController<PropertyLabelTranslate, PropertyLabelTranslateDto>
        , IGetFromTenant<PropertyLabelTranslateDto>
    {
        public PropertyLabelTranslateController(IModelService<PropertyLabelTranslate, PropertyLabelTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<PropertyLabelTranslate>, IQueryable<PropertyLabelTranslateDto>> DtoConverter
        => items => items.Select(i => new PropertyLabelTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            Language = i.Language,
            LanguageId = i.LanguageId,
            PropertyLabelId = i.PropertyLabelId,
            PropertyLabel = i.PropertyLabel,
        });

        [HttpGet("PropertyLabelTranslates/{tenantId}")]
        public async Task<ActionResult<IEnumerable<PropertyLabelTranslateDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken)
        {
            var res = await ModelService.DataConvertQuery(ModelService
                    .AsQueryable(i => i.PropertyLabelId == tenantId))
                .ToListAsync(cancellationToken);
            return res;
        }
    }
}

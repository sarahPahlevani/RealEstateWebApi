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
    public class PropertyFeatureTranslateController : ModelController<PropertyFeatureTranslate, PropertyFeatureTranslateDto>
        , IGetFromTenant<PropertyFeatureTranslateDto>

    {
        public PropertyFeatureTranslateController(IModelService<PropertyFeatureTranslate, PropertyFeatureTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<PropertyFeatureTranslate>, IQueryable<PropertyFeatureTranslateDto>> DtoConverter
        => items => items.Select(i => new PropertyFeatureTranslateDto
        {
            Id = i.Id,
            PropertyFeatureId = i.PropertyFeatureId,
            PropertyFeature = i.PropertyFeature,
            LanguageId = i.LanguageId,
            Language = i.Language,
            Name = i.Name,
        });

        [HttpGet("PropertyFeatureTranslates/{tenantId}")]
        public async Task<ActionResult<IEnumerable<PropertyFeatureTranslateDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken)
        {
            var res = await ModelService.DataConvertQuery(ModelService.AsQueryable(i => i.PropertyFeatureId == tenantId))
                .ToListAsync(cancellationToken);
            return res;
        }
    }
}

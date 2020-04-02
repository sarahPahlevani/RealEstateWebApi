
﻿using Microsoft.AspNetCore.Mvc;
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
    public class PropertyStatusTranslateController : ModelController<PropertyStatusTranslate, PropertyStatusTranslateDto>
        , IGetFromTenant<PropertyStatusTranslateDto>
    {
        public PropertyStatusTranslateController(IModelService<PropertyStatusTranslate, PropertyStatusTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<PropertyStatusTranslate>, IQueryable<PropertyStatusTranslateDto>> DtoConverter
        => items => items.Select(i => new PropertyStatusTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            Language = i.Language,
            PropertyStatusId = i.PropertyStatusId,
            PropertyStatus = i.PropertyStatus,
        });

        [HttpGet("PropertyStatusTranslates/{tenantId}")]
        public async Task<ActionResult<IEnumerable<PropertyStatusTranslateDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken)
        {
            var res = await ModelService.DataConvertQuery(ModelService
                    .AsQueryable(i => i.PropertyStatusId == tenantId))
                .ToListAsync(cancellationToken);
            return res;
        }
    }
}

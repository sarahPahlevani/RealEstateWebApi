using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class ImportantPlaceTypeTranslateController
        : ModelController<ImportantPlaceTypeTranslate, ImportantPlaceTypeTranslateDto>
            ,IGetFromTenant<ImportantPlaceTypeTranslateDto>
    {

        public ImportantPlaceTypeTranslateController(
            IModelService<ImportantPlaceTypeTranslate, ImportantPlaceTypeTranslateDto> modelService)
            : base(modelService)
        {
        }

        public override Func<IQueryable<ImportantPlaceTypeTranslate>, IQueryable<ImportantPlaceTypeTranslateDto>>
            DtoConverter => entities => entities.Select(i => new ImportantPlaceTypeTranslateDto
            {
                Name = i.Name,
                Id = i.Id,
                LanguageId = i.LanguageId,
                ImportantPlaceTypeId = i.ImportantPlaceTypeId
            });


        [HttpGet("ImportantPlaceTypeTranslates/{tenantId}")]
        public async Task<ActionResult<IEnumerable<ImportantPlaceTypeTranslateDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken)
        {
            var res = await ModelService.DataConvertQuery(ModelService
                    .AsQueryable(i => i.ImportantPlaceTypeId == tenantId))
                .ToListAsync(cancellationToken);
            return res;
        }
    }
}

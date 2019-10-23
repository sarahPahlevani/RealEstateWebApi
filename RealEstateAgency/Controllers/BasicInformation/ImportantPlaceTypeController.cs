using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class ImportantPlaceTypeController
        : ModelPagingController<ImportantPlaceType, ImportantPlaceTypeDto, ImportantPlaceTypeDto>
    {
        private readonly IUploadHelperService _uploadHelperService;
        private readonly IPathProvider _pathProvider;

        public ImportantPlaceTypeController(IModelService<ImportantPlaceType, ImportantPlaceTypeDto> modelService
            , IUploadHelperService uploadHelperService,IPathProvider pathProvider) : base(modelService)
        {
            _uploadHelperService = uploadHelperService;
            _pathProvider = pathProvider;
        }

        private Func<IQueryable<ImportantPlaceType>, IQueryable<ImportantPlaceTypeDto>> _converter
        => entities => entities.Select(i => new ImportantPlaceTypeDto
        {
            Name = i.Name,
            Id = i.Id,
            TypeIcon = i.TypeIcon
        });

        public override Func<IQueryable<ImportantPlaceType>, IQueryable<ImportantPlaceTypeDto>> PagingConverter =>
            _converter;

        public override Func<IQueryable<ImportantPlaceType>, IQueryable<ImportantPlaceTypeDto>> DtoConverter =>
            _converter;

        public override async Task<ActionResult<IEnumerable<ImportantPlaceTypeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var res = await DtoConverter(ModelService.Queryable).ToListAsync(cancellationToken);
            res.ForEach(i =>
            {
                i.TypeIconImageUrl = _pathProvider.GetImageApiPath<ImportantPlaceType>(nameof(ImportantPlaceType.TypeIcon),i.Id.ToString());
                i.TypeIcon = null;
            });
            return res;
        }

        public override async Task<ActionResult<ImportantPlaceTypeDto>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var floorPlan = await ModelService.GetDtoAsync(id, cancellationToken);
            if (floorPlan is null) return NotFound();
            floorPlan.TypeIconImageUrl =
                _pathProvider.GetImageApiPath<ImportantPlaceType>(nameof(ImportantPlaceType.TypeIcon)
                    , floorPlan.Id.ToString());
            floorPlan.TypeIcon = null;
            return floorPlan;
        }

        public override Task<ActionResult<ImportantPlaceTypeDto>> Create(ImportantPlaceTypeDto value, CancellationToken cancellationToken)
            => throw new InvalidOperationException("please use the uploadAndCreate method instead");

        public override async Task<ActionResult> UpdateAsync(ImportantPlaceTypeDto value, CancellationToken cancellationToken)
        {
            await ModelService.UpdateByDtoAsync(value, cancellationToken);
            return NoContent();
        }

        [HttpPatch("[Action]"), DisableRequestSizeLimit]
        public async Task<ActionResult<ImportantPlaceTypeDto>> UploadAndCreate(CancellationToken cancellationToken)
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length <= 0) throw new AppException("file is not set");
            var placeTypeDto = new ImportantPlaceTypeDto
            {
                Name = Request.Form["Name"],
                TypeIcon = await _uploadHelperService
                    .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken)
            };
            return await ModelService.CreateByDtoAsync(placeTypeDto, cancellationToken);
        }
    }
}

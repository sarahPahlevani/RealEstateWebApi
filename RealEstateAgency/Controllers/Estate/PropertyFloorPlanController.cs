using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.Providers;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyFloorPlanController : ModelController<PropertyFloorPlan, PropertyFloorPlanDto>
    {
        private readonly IUploadHelperService _uploadHelperService;
        private readonly IPathProvider _pathProvider;

        public PropertyFloorPlanController(IModelService<PropertyFloorPlan, PropertyFloorPlanDto> modelService,
            IUploadHelperService uploadHelperService,IPathProvider pathProvider) : base(modelService)
        {
            _uploadHelperService = uploadHelperService;
            _pathProvider = pathProvider;
        }

        public override Func<IQueryable<PropertyFloorPlan>, IQueryable<PropertyFloorPlanDto>> DtoConverter
        => items => items.Select(i => new PropertyFloorPlanDto
        {
            Id = i.Id,
            PropertyId = i.PropertyId,
            FloorName = i.FloorName,
            FloorPrice = i.FloorPrice,
            PricePostfix = i.PricePostfix,
            FloorSize = i.FloorSize,
            SizePostfix = i.SizePostfix,
            Bedrooms = i.Bedrooms,
            Bathrooms = i.Bathrooms,
            PlanDescription = i.PlanDescription,
            ImageCaption = i.ImageCaption,
            UploadDate = i.UploadDate,
            ImageExtension = i.ImageExtension,
            ImageSize = i.ImageSize,
            ImagePath = i.ImagePath,
            TumbPath = i.TumbPath,
            Is360View = i.Is360View,
        });

        public override async Task<ActionResult<IEnumerable<PropertyFloorPlanDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var res = (await ModelService.GetAllDtosAsync(cancellationToken)).ToList();
            //res.ForEach(i =>
            //{
            //    i.ImageContentTumblrUrl = _pathProvider.GetImageApiPath<PropertyFloorPlan>(nameof(PropertyFloorPlan.ImageContentTumblr),i.Id.ToString());
            //    i.ImageContentTumblr = null;
            //});
            return res;
        }

        public override async Task<ActionResult<PropertyFloorPlanDto>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var floorPlan = await ModelService.GetDtoAsync(id, cancellationToken);
            if (floorPlan is null) return NotFound();
            //floorPlan.ImageContentTumblrUrl = _pathProvider.GetImageApiPath<PropertyFloorPlan>(
            //    nameof(PropertyFloorPlan.ImageContentTumblr),
            //    floorPlan.Id.ToString()); 
            //floorPlan.ImageContentTumblr = null;
            return floorPlan;
        }

        [HttpGet("[Action]/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyFloorPlanDto>>> GetPropertyFloorPlans(int propertyId, CancellationToken cancellationToken)
            => await ModelService.DataConvertQuery(ModelService.AsQueryable(i => i.PropertyId == propertyId)).ToListAsync(cancellationToken);

        [HttpPost("[Action]"), DisableRequestSizeLimit]
        public async Task<ActionResult<PropertyFloorPlanDto>> SetPropertyFloorPlanImage(CancellationToken cancellationToken)
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length <= 0) throw new AppException("file is not set");

            var propertyFloorPlanId = int.Parse(Request.Form["propertyFloorPlanId"]);
            var floorPlan = await ModelService.GetAsync(propertyFloorPlanId, cancellationToken);
            if (floorPlan == null) throw new AppException("not found floor plan");

            var fileInfo = _uploadHelperService.FileService.GetFileInfo(file.FileName, file.Length);

            var imageFull = await _uploadHelperService
                    .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken);
            var imageTumb = await _uploadHelperService
                    .ImageService.GetSmallerImageBytes(file.OpenReadStream(), cancellationToken);

            var imagePath = _pathProvider.GetImagePhysicalPath<PropertyFloorPlan>(nameof(PropertyFloorPlanDto.ImageFull),
                        floorPlan.Id.ToString());
            var tumbPath = _pathProvider.GetImagePhysicalPath<PropertyFloorPlan>(nameof(PropertyFloorPlanDto.ImageTumb),
                    floorPlan.Id.ToString());

            _uploadHelperService.ImageService.SaveImage(imageFull, imagePath);
            _uploadHelperService.ImageService.SaveSmallerImage(imageTumb, tumbPath);

            floorPlan.ImagePath = _pathProvider.GetImageVirtualPath<PropertyFloorPlan>(nameof(PropertyFloorPlanDto.ImageFull),
                    floorPlan.Id.ToString());
            floorPlan.TumbPath = _pathProvider.GetImageVirtualPath<PropertyFloorPlan>(nameof(PropertyFloorPlanDto.ImageTumb),
                    floorPlan.Id.ToString());

            floorPlan.UploadDate = DateTime.UtcNow;
            floorPlan.ImageExtension = fileInfo.FileExtension;
            floorPlan.ImageSize = fileInfo.FileSize;
            await ModelService.UpdateAsync(floorPlan, cancellationToken);

            return new PropertyFloorPlanDto
            {
                Id = floorPlan.Id,
                PropertyId = floorPlan.PropertyId,
                FloorName = floorPlan.FloorName,
                FloorPrice = floorPlan.FloorPrice,
                PricePostfix = floorPlan.PricePostfix,
                FloorSize = floorPlan.FloorSize,
                SizePostfix = floorPlan.SizePostfix,
                Bedrooms = floorPlan.Bedrooms,
                Bathrooms = floorPlan.Bathrooms,
                PlanDescription = floorPlan.PlanDescription,
                ImageCaption = floorPlan.ImageCaption,
                UploadDate = floorPlan.UploadDate,
                ImageExtension = fileInfo.FileExtension,
                ImageSize = fileInfo.FileSize,
                ImagePath = floorPlan.ImagePath,
                TumbPath = floorPlan.TumbPath,
                Is360View = floorPlan.Is360View,
            };
        }
    }
}

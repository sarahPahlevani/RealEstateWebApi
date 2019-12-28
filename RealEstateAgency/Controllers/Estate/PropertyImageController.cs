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
    public class PropertyImageController : BaseApiController
    {
        private readonly IEntityService<PropertyImage> _entityService;
        private readonly IUploadHelperService _uploadHelperService;
        private readonly IPathProvider _pathProvider;
        private readonly IEntityService<Property> _propertyService;

        public enum ImageType
        {
            Full,
            Tumblr
        }

        public PropertyImageController(IEntityService<PropertyImage> entityService
            , IUploadHelperService uploadHelperService
            , IPathProvider pathProvider
            , IEntityService<Property> propertyService)
        {
            _entityService = entityService;
            _uploadHelperService = uploadHelperService;
            _pathProvider = pathProvider;
            _propertyService = propertyService;
        }

        [AllowAnonymous]
        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(int propertyId,
            CancellationToken cancellationToken) =>
            await GetImagesDto(propertyId, cancellationToken);

        [HttpGet("{propertyId}/{id}")]
        public async Task<ActionResult<PropertyImageDto>> GetPropertyImage(int propertyId,
            int id, CancellationToken cancellationToken) =>
        ConvertToDto(await _entityService.Queryable
        .FirstOrDefaultAsync(i => i.PropertyId == propertyId && i.Id == id
        , cancellationToken));

        private async Task<List<PropertyImageDto>> GetImagesDto(int propertyId, CancellationToken cancellationToken = default)
        {
            var imageModels = await _entityService.AsQueryable(i => i.PropertyId == propertyId)
                .OrderBy(i => i.Priority)
                .ToListAsync(cancellationToken);
            var dtos = new List<PropertyImageDto>();
            imageModels.ForEach(i => dtos.Add(ConvertToDto(i)));
            return dtos;
        }

        [AllowAnonymous]
        [HttpGet("[Action]/{propertyId}")]
        public async Task<List<PropertyImageDto>> Get360Images(int propertyId, CancellationToken cancellationToken = default)
        {
            var imageModels = await _entityService.AsQueryable(i => i.PropertyId == propertyId && !i.Deleted && i.Is360View)
                .OrderBy(i => i.Priority)
                .ToListAsync(cancellationToken);
            var dtos = new List<PropertyImageDto>();
            imageModels.ForEach(i => dtos.Add(ConvertToDto(i)));
            return dtos;
        }

        private PropertyImageDto ConvertToDto(PropertyImage propertyImage) =>
            new PropertyImageDto
            {
                Id = propertyImage.Id,
                PropertyId = propertyImage.PropertyId,
                UploadDate = propertyImage.UploadDate,
                ImageExtension = propertyImage.ImageExtension,
                ImageSize = propertyImage.ImageSize,
                ImageCaption = propertyImage.ImageCaption,
                ImagePath = propertyImage.ImagePath,
                TumbPath = propertyImage.TumbPath,
                //ImageUrl = _pathProvider.GetImageApiPath<PropertyImage>(getFullImage
                //    ? nameof(PropertyImage.ImageContentFull) : nameof(PropertyImage.ImageContentTumblr)
                //    , propertyImage.Id.ToString()),
                Is360View = propertyImage.Is360View,
                Priority = propertyImage.Priority
            };

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id,
            CancellationToken cancellationToken)
        {
            await _entityService.DeleteAsync(await _entityService.GetAsync(id, cancellationToken),
                cancellationToken);
            return NoContent();
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<PropertyImageDto>> Create(CancellationToken cancellationToken)
        {
            if (Request.Form is null) throw new AppException("form is null");

            if (Request.Form.Files.Count != 1) throw new AppException("files are not set OR more than one");

            var image = await GetImageFromFormData(cancellationToken);

            var file = _entityService.AsQueryable(r => r.Id == image.Id).FirstOrDefault();

            var imagePath = _pathProvider.GetImagePhysicalPath<PropertyImage>(nameof(PropertyImageDto.ImageContentFull),
                        file.Id.ToString());
            var tumbPath = _pathProvider.GetImagePhysicalPath<PropertyImage>(nameof(PropertyImageDto.ImageContentTumblr),
                    file.Id.ToString());

            _uploadHelperService.ImageService.SaveImage(image.ImageContentFull, imagePath);
            _uploadHelperService.ImageService.SaveSmallerImage(image.ImageContentTumblr, tumbPath);

            file.ImagePath = _pathProvider.GetImageVirtualPath<PropertyImage>(nameof(PropertyImageDto.ImageContentFull),
                    file.Id.ToString());
            file.TumbPath = _pathProvider.GetImageVirtualPath<PropertyImage>(nameof(PropertyImageDto.ImageContentTumblr),
                    file.Id.ToString());

            _entityService.DbContext.Entry(file).State = EntityState.Modified;
            await _entityService.DbContext.SaveChangesAsync();

            var property = await _propertyService.GetAsync(file.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return new PropertyImageDto
            {
                Id = file.Id,
                PropertyId = file.PropertyId,
                UploadDate = file.UploadDate,
                ImageCaption = file.ImageCaption,
                ImageSize = file.ImageSize,
                ImageExtension = file.ImageExtension,
                ImagePath = file.ImagePath,
                TumbPath = file.TumbPath,
                Is360View = file.Is360View,
                Priority = file.Priority
            };
        }

        private async Task<PropertyImageDto> GetImageFromFormData(CancellationToken cancellationToken)
        {
            var propertyId = int.Parse(Request.Form["propertyId"]);
            var imageCaption = Request.Form["imageCaption"];
            var is360View = !string.IsNullOrWhiteSpace(Request.Form["is360View"])
                            && bool.Parse(Request.Form["is360View"]);
            var priority = string.IsNullOrWhiteSpace(Request.Form["priority"]) ? 0 :
                             int.Parse(Request.Form["priority"]);

            var file = Request.Form.Files[0];
            var fileInfo = _uploadHelperService.FileService.GetFileInfo(file.FileName, file.Length);
            if (!_uploadHelperService.ImageService.IsValidImageExtension(fileInfo.FileExtension))
                throw new InvalidOperationException(
                    $"The extension {fileInfo.FileExtension} is not valid for image");

            var image = new PropertyImage();
            image.PropertyId = propertyId;
            image.ImageCaption = imageCaption;
            image.ImageExtension = fileInfo.FileExtension;
            image.ImageSize = fileInfo.FileSize;
            image.Is360View = is360View;
            image.UploadDate = DateTime.UtcNow;
            image.Priority = priority;
            _entityService.DbContext.Add(image);
            await _entityService.DbContext.SaveChangesAsync();

            return new PropertyImageDto
            {
                Id = image.Id,
                PropertyId = image.PropertyId,
                ImageCaption = image.ImageCaption,
                ImageExtension = fileInfo.FileExtension,
                ImageSize = fileInfo.FileSize,
                Is360View = image.Is360View,
                UploadDate = image.UploadDate,
                Priority = image.Priority,
                ImageContentFull = await _uploadHelperService
                    .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken),
                ImageContentTumblr = await _uploadHelperService
                    .ImageService.GetSmallerImageBytes(file.OpenReadStream(), cancellationToken)
            };
        }

        //private async Task<List<PropertyImage>> GetImagesFromFormData(CancellationToken cancellationToken)
        //{
        //    var propertyId = int.Parse(Request.Form["propertyId"]);
        //    var imageCaption = Request.Form["imageCaption"];
        //    var is360View = !string.IsNullOrWhiteSpace(Request.Form["is360View"])
        //                    && bool.Parse(Request.Form["is360View"]);
        //    var priority = string.IsNullOrWhiteSpace(Request.Form["priority"]) ? 0 :
        //                     int.Parse(Request.Form["priority"]);
        //    var imageList = new List<PropertyImage>();
        //    foreach (var file in Request.Form.Files)
        //    {
        //        var fileInfo = _uploadHelperService.FileService.GetFileInfo(file.FileName, file.Length);

        //        if (!_uploadHelperService.ImageService.IsValidImageExtension(fileInfo.FileExtension))
        //            throw new InvalidOperationException(
        //                $"The extension {fileInfo.FileExtension} is not valid for image");

        //        var dto = new PropertyImage
        //        {
        //            PropertyId = propertyId,
        //            ImageCaption = imageCaption,
        //            ImageExtension = fileInfo.FileExtension,
        //            ImageSize = fileInfo.FileSize,
        //            Is360View = is360View,
        //            UploadDate = DateTime.Now,
        //            Priority = priority,
        //            //ImageContentFull = await _uploadHelperService
        //            //    .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken),
        //            //ImageContentTumblr = await _uploadHelperService
        //            //    .ImageService.GetSmallerImageBytes(file.OpenReadStream(), cancellationToken),
        //        };
        //        imageList.Add(dto);
        //    }
        //    _entityService.DbContext.AddRange(imageList);
        //    await _entityService.DbContext.SaveChangesAsync();

        //    foreach (var item in imageList)
        //    {
        //        item.ImageContentFull = await _uploadHelperService
        //                .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken);
        //            item.ImageContentTumblr = await _uploadHelperService
        //                .ImageService.GetSmallerImageBytes(file.OpenReadStream(), cancellationToken);
        //    }

        //    return imageList;
        //}

    }
}

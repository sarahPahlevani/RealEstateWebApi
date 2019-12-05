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

        public enum ImageType
        {
            Full,
            Tumblr
        }

        public PropertyImageController(IEntityService<PropertyImage> entityService
            , IUploadHelperService uploadHelperService
            , IPathProvider pathProvider)
        {
            _entityService = entityService;
            _uploadHelperService = uploadHelperService;
            _pathProvider = pathProvider;
        }

        [HttpGet("{imageType}/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(int propertyId,
            ImageType imageType,
            CancellationToken cancellationToken) =>
            await GetImagesDto(propertyId, cancellationToken, imageType == ImageType.Full);

        [HttpGet("{imageType}/{propertyId}/{id}")]
        public async Task<ActionResult<PropertyImageDto>> GetPropertyImage(int propertyId,
            ImageType imageType, int id, CancellationToken cancellationToken) =>
        ConvertToDto(imageType == ImageType.Full, await _entityService.Queryable
        .FirstOrDefaultAsync(i => i.PropertyId == propertyId && i.Id == id
        , cancellationToken));

        private async Task<List<PropertyImageDto>> GetImagesDto(int propertyId,
            CancellationToken cancellationToken = default, bool getFullImage = false)
        {
            var imageModels = await _entityService.AsQueryable(i => i.PropertyId == propertyId)
                .OrderBy(i => i.Priority)
                .ToListAsync(cancellationToken);
            var dtos = new List<PropertyImageDto>();
            imageModels.ForEach(i => dtos.Add(ConvertToDto(getFullImage, i)));
            return dtos;
        }

        private PropertyImageDto ConvertToDto(bool getFullImage, PropertyImage propertyImage) =>
            new PropertyImageDto
            {
                Id = propertyImage.Id,
                PropertyId = propertyImage.Id,
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


        //[AllowAnonymous]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<List<PropertyImageDto>>> Create(CancellationToken cancellationToken)
        {
            if(Request.Form is null) throw new AppException("form is null");

            if (Request.Form.Files.Count == 0) throw new AppException("files are not set");

            var files = await GetImagesFromFormData(cancellationToken);
            _entityService.DbContext.AddRange(files);
            files.ForEach(file =>
            {
                var imagePath = _pathProvider.GetImagePhysicalPath<PropertyImage>(nameof(PropertyImage.ImageContentFull),
                        file.Id.ToString());
                var tumbPath = _pathProvider.GetImagePhysicalPath<PropertyImage>(nameof(PropertyImage.ImageContentTumblr),
                        file.Id.ToString());

                _uploadHelperService.ImageService.SaveImage(file.ImageContentFull, imagePath);
                _uploadHelperService.ImageService.SaveSmallerImage(file.ImageContentTumblr, tumbPath);
                file.ImageContentFull = null;
                file.ImageContentTumblr = null;

                file.ImagePath = _pathProvider.GetImageVirtualPath<PropertyImage>(nameof(PropertyImage.ImageContentFull),
                        file.Id.ToString());
                file.TumbPath = _pathProvider.GetImageVirtualPath<PropertyImage>(nameof(PropertyImage.ImageContentTumblr),
                        file.Id.ToString());
            });

            await _entityService.DbContext.SaveChangesAsync();

            return files.Select(f => new PropertyImageDto
            {
                Id = f.Id,
                PropertyId = f.PropertyId,
                UploadDate = f.UploadDate,
                ImageCaption = f.ImageCaption,
                ImageSize = f.ImageSize,
                ImageExtension = f.ImageExtension,
                ImagePath = f.ImagePath,
                TumbPath = f.TumbPath,
                Is360View = f.Is360View,
                Priority = f.Priority
            }).ToList();
        }

        private async Task<List<PropertyImage>> GetImagesFromFormData(CancellationToken cancellationToken)
        {
            var propertyId = int.Parse(Request.Form["propertyId"]);
            var imageCaption = Request.Form["imageCaption"];
            var is360View = !string.IsNullOrWhiteSpace(Request.Form["is360View"])
                            && bool.Parse(Request.Form["is360View"]);
            var priority = string.IsNullOrWhiteSpace(Request.Form["priority"]) ? 0 :
                             int.Parse(Request.Form["priority"]);
            var imageList = new List<PropertyImage>();
            foreach (var file in Request.Form.Files)
            {
                var fileInfo = _uploadHelperService.FileService.GetFileInfo(file.FileName, file.Length);

                if (!_uploadHelperService.ImageService.IsValidImageExtension(fileInfo.FileExtension))
                    throw new InvalidOperationException(
                        $"The extension {fileInfo.FileExtension} is not valid for image");

                var dto = new PropertyImage
                {
                    PropertyId = propertyId,
                    ImageCaption = imageCaption,
                    ImageExtension = fileInfo.FileExtension,
                    ImageSize = fileInfo.FileSize,
                    Is360View = is360View,
                    UploadDate = DateTime.Now,
                    Priority = priority,
                    ImageContentFull = await _uploadHelperService
                        .ImageService.GetImageBytes(file.OpenReadStream(), cancellationToken),
                    ImageContentTumblr = await _uploadHelperService
                        .ImageService.GetSmallerImageBytes(file.OpenReadStream(), cancellationToken),
                };
                imageList.Add(dto);
            }

            return imageList;
        }
    }
}

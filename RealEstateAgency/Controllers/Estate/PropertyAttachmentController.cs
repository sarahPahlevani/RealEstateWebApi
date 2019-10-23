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
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyAttachmentController : BaseApiController
    {
        private readonly IUploadHelperService _uploadHelperService;
        private readonly IEntityService<PropertyAttachment> _entityService;
        private readonly IDownloadHelperService _downloadHelperService;

        public PropertyAttachmentController(IUploadHelperService uploadHelperService
            , IEntityService<PropertyAttachment> entityService
            , IDownloadHelperService downloadHelperService)
        {
            _uploadHelperService = uploadHelperService;
            _entityService = entityService;
            _downloadHelperService = downloadHelperService;
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyAttachmentDto>>> GetPropertyAttachments(int propertyId,
            CancellationToken cancellationToken) =>
            await _entityService.AsQueryable(i => i.PropertyId == propertyId)
                .Select(a => new PropertyAttachmentDto
                {
                    Id = a.Id,
                    PropertyId = a.PropertyId,
                    UploadDate = a.UploadDate,
                    FileExtension = a.FileExtension,
                    FileSize = a.FileSize,
                    FileCaption = a.FileCaption
                }).ToListAsync(cancellationToken);

        [HttpGet("[Action]/{id}")]
        public async Task<ActionResult> Download(int id
            , CancellationToken cancellationToken)
        {
            var att = await _entityService.GetAsync(id, cancellationToken);
            var dataStream = new MemoryStream(att.FileContent);
            return new FileStreamResult(dataStream,
                _downloadHelperService.GetExtensionContentTypeHeader(att.FileExtension));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _entityService.DeleteAsync(await _entityService.GetAsync(id, cancellationToken),
                cancellationToken);
            return NoContent();
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<List<PropertyAttachmentDto>>> Create(CancellationToken cancellationToken)
        {
            if (Request.Form.Files.Count == 0) throw new AppException("files are not set");

            var files = await GetAttachmentsFromFormData(cancellationToken);
            _entityService.DbContext.AddRange(files);
            await _entityService.DbContext.SaveChangesAsync(cancellationToken);
            return files.Select(f => new PropertyAttachmentDto
            {
                Id = f.Id,
                PropertyId = f.PropertyId,
                UploadDate = f.UploadDate,
                FileCaption = f.FileCaption,
                FileSize = f.FileSize,
                FileExtension = f.FileExtension,
            }).ToList();
        }

        private async Task<List<PropertyAttachment>> GetAttachmentsFromFormData(CancellationToken cancellationToken)
        {
            var propertyId = int.Parse(Request.Form["propertyId"]);
            var fileCaption = Request.Form["fileCaption"];
            var attList = new List<PropertyAttachment>();
            foreach (var file in Request.Form.Files)
            {
                var fileInfo = _uploadHelperService.FileService.GetFileInfo(file.FileName, file.Length);

                if (!_uploadHelperService.FileService.IsValidDocumentExtension(fileInfo.FileExtension))
                    throw new InvalidOperationException(
                        $"The extension {fileInfo.FileExtension} is not valid");

                var dto = new PropertyAttachment
                {
                    PropertyId = propertyId,
                    FileCaption = fileInfo.FileName,
                    FileExtension = fileInfo.FileExtension,
                    FileSize = fileInfo.FileSize,
                    UploadDate = DateTime.Now,
                    FileContent = await _uploadHelperService.FileService
                        .GetFileBytes(file.OpenReadStream(), cancellationToken)
                };
                attList.Add(dto);
            }

            return attList;
        }
    }
}

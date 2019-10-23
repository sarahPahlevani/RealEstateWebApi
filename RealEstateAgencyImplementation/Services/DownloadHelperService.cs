using RealEstateAgency.Shared.Statics;
using System;
using RealEstateAgency.Shared.Exceptions;

namespace RealEstateAgency.Shared.Services
{
    public class DownloadHelperService : IDownloadHelperService
    {
        private readonly IFileStatics _fileStatics;

        public DownloadHelperService(IFileStatics fileStatics) => _fileStatics = fileStatics;

        public string GetExtensionContentTypeHeader(string extension)
        {
            var contentType = _fileStatics.FileExtensionsContentType[extension];
            if (contentType is null)
                throw new InvalidExtensionException(extension);
            return contentType;
        }
    }

    public interface IDownloadHelperService
    {
        string GetExtensionContentTypeHeader(string extension);
    }
}

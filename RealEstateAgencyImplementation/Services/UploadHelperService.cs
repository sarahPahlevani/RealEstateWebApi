using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Shared.Services
{
    public class UploadHelperService : IUploadHelperService
    {
        private readonly IFastHasher _hasher;
        public IFileService FileService { get; }
        public IImageService ImageService { get; }

        public UploadHelperService(IFileService fileService, IImageService imageService, IFastHasher hasher)
        {
            _hasher = hasher;
            FileService = fileService;
            ImageService = imageService;
        }

        public Task<byte[]> UploadImage(Stream file, string filePath, CancellationToken token) 
            => ImageService.SaveImage(file, filePath, token);

        public Task<byte[]> UploadSmallerImage(Stream file, string filePath, CancellationToken token)
            => ImageService.SaveSmallerImage(file, filePath, token);
    }

    public interface IUploadHelperService
    {
        IFileService FileService { get; }
        IImageService ImageService { get; }
        Task<byte[]> UploadImage(Stream file, string filePath, CancellationToken token);
        Task<byte[]> UploadSmallerImage(Stream file, string filePath, CancellationToken token);
    }
}

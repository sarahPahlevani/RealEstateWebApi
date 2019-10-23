using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Statics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Shared.Services
{
    //can be change to prototype design pattern

    public class FileService : IFileService
    {
        private readonly IFileStatics _fileStatics;

        public FileService(IFileStatics fileStatics) => _fileStatics = fileStatics;

        public UploadFileInfo GetFileInfo(string fileName, long fileSize)
        {
            return new UploadFileInfo
            {
                FileName = Path.GetFileName(fileName),
                FileExtension = Path.GetExtension(fileName),
                FileSize = (int)fileSize
            };
        }

        public async Task<byte[]> GetFileBytesAsync(Stream fileStream, CancellationToken cancellationToken)
        {
            var result = new byte[fileStream.Length];
            await fileStream.ReadAsync(result, 0, (int)fileStream.Length, cancellationToken);
            return result;
        }

        public bool IsValidDocumentExtension(string extension)
            => _fileStatics.DocumentFileExtensions.Contains(extension.Trim().ToLower());

        public async Task<byte[]> GetFileBytes(Stream stream, CancellationToken token = default)
        {
            var result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length, token);
            return result;
        }
    }

    public interface IFileService
    {
        UploadFileInfo GetFileInfo(string fileName, long fileSize);

        Task<byte[]> GetFileBytesAsync(Stream fileStream, CancellationToken cancellationToken);

        Task<byte[]> GetFileBytes(Stream stream, CancellationToken token);

        bool IsValidDocumentExtension(string extension);
    }
}

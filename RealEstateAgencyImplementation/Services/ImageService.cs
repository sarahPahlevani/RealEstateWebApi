using RealEstateAgency.Shared.Statics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace RealEstateAgency.Shared.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileStatics _fileStatics;

        public ImageService(IFileStatics fileStatics) => _fileStatics = fileStatics;

        public int ImageDivisionFactor => 2;
        public int ImageQualityFactor => 75;

        public async Task<byte[]> GetImageBytes(Stream stream, CancellationToken token = default)
        {
            var result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length, token);
            return result;
        }

        public async Task<byte[]> GetSmallerImageBytes(Stream stream, CancellationToken token = default)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var image = Image.Load<Rgb24>(stream))
            {
                var newImage = image.Clone();
                ResizeForSmallerImage(newImage);

                var imageMemoryStream = new MemoryStream();
                newImage.SaveAsJpeg(imageMemoryStream);
                imageMemoryStream.Seek(0, SeekOrigin.Begin);
                var result = new byte[imageMemoryStream.Length];
                await imageMemoryStream.ReadAsync(result, 0, (int)imageMemoryStream.Length, token);

                return result;
            }
        }

        public Task<byte[]> SaveImage(Stream stream, string filePath, CancellationToken token = default)
        {
            CheckDirectoryExist(filePath);
            stream.Seek(0, SeekOrigin.Begin);
            using (var image = Image.Load<Rgb24>(stream))
            {
                image.Save(filePath, new JpegEncoder());
            }
            return GetImageBytes(stream, token);
        }

        public Task<byte[]> SaveSmallerImage(Stream stream, string filePath, CancellationToken token = default)
        {
            stream.Seek(0, SeekOrigin.Begin);
            CheckDirectoryExist(filePath);
            using (var image = Image.Load<Rgb24>(stream))
            {
                ResizeForSmallerImage(image);
                image.Save(filePath, new JpegEncoder());

                var newImage = image.Clone();
                var imageMemoryStream = new MemoryStream();
                newImage.SaveAsJpeg(imageMemoryStream);
                imageMemoryStream.Seek(0, SeekOrigin.Begin);
                return GetImageBytes(imageMemoryStream, CancellationToken.None);
            }
        }
        public void SaveImage(byte[] file, string filePath)
        {
            CheckDirectoryExist(filePath);
            using (var image = Image.Load<Rgb24>(file))
            {
                image.Save(filePath, new JpegEncoder());
            }
        }

        public void SaveSmallerImage(byte[] file, string filePath)
        {
            CheckDirectoryExist(filePath);
            using (var image = Image.Load<Rgb24>(file))
            {
                ResizeForSmallerImage(image);
                image.Save(filePath, new JpegEncoder());
            }
        }

        public string GetImageBase64(byte[] imageBuffer)
            => imageBuffer is null ? "" : Convert.ToBase64String(imageBuffer);

        public bool IsValidImageExtension(string extension)
            => _fileStatics.ImageFileExtensions.Contains(extension.Trim().ToLower());

        private void ResizeForSmallerImage(Image<Rgb24> newImage) =>
            newImage.Mutate(i => i.Resize(newImage.Width / ImageDivisionFactor
                , newImage.Height / ImageDivisionFactor));

        private void CheckDirectoryExist(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

    }

    public interface IImageService
    {
        int ImageDivisionFactor { get; }
        int ImageQualityFactor { get; }
        void SaveImage(byte[] file, string filePath);
        void SaveSmallerImage(byte[] file, string filePath);
        Task<byte[]> SaveImage(Stream stream, string filePath, CancellationToken token = default);
        Task<byte[]> SaveSmallerImage(Stream stream, string filePath, CancellationToken token = default);
        Task<byte[]> GetImageBytes(Stream stream, CancellationToken token = default);
        Task<byte[]> GetSmallerImageBytes(Stream stream, CancellationToken token = default);
        string GetImageBase64(byte[] imageBuffer);
        bool IsValidImageExtension(string extension);
    }
}

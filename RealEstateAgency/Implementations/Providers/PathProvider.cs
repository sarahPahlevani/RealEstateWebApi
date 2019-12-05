using Microsoft.Extensions.Options;
using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Services;
using System.IO;

namespace RealEstateAgency.Implementations.Providers
{
    public class PathProvider : IPathProvider
    {
        private readonly IFastHasher _hasher;
        private readonly AppSetting _appSetting;

        public PathProvider(IFastHasher hasher, IOptions<AppSetting> options)
        {
            _hasher = hasher;
            _appSetting = options.Value;
            if (_appSetting.ApiBaseUrl.EndsWith("/"))
                _appSetting.ApiBaseUrl =
                    _appSetting.ApiBaseUrl.Substring(0, _appSetting.ApiBaseUrl.Length - 1);
        }

        public string ApplicationPath => Startup.ApplicationPath;
        public string ImagesUploadDirectory => Path.Combine(ApplicationPath, @"images");

        private string GetImagePhysicalPath(string category, string key)
            => $"{ImagesUploadDirectory}\\{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImagePhysicalPath<T>(string property, string key)
            => GetImagePhysicalPath($"{typeof(T).Name}_{property}", key);

        private string GetImageApiPath(string category, string key)
            => $"{_appSetting.ApiBaseUrl}/images/{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImageApiPath<T>(string property, string key)
            => GetImageApiPath($"{typeof(T).Name}_{property}", key);

        private string GetImageVirtualPath(string category, string key)
            => $"/images/{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImageVirtualPath<T>(string property, string key)
            => GetImageVirtualPath($"{typeof(T).Name}_{property}", key);
    }

    public interface IPathProvider
    {
        string ApplicationPath { get; }
        string ImagesUploadDirectory { get; }

        string GetImagePhysicalPath<T>(string property, string key);

        string GetImageApiPath<T>(string property, string key);

        string GetImageVirtualPath<T>(string property, string key);
    }
}

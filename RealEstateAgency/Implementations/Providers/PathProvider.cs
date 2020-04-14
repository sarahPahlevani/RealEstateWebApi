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

        private string GetImagePhysicalPath(string directory, string category, string key)
            => $"{ImagesUploadDirectory}\\{directory}\\{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImagePhysicalPath<T>(string category, string key)
            => GetImagePhysicalPath(typeof(T).Name, category, key);

        private string GetImageApiPath(string directory, string category, string key)
            => $"{_appSetting.ApiBaseUrl}/images/{directory}/{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImageApiPath<T>(string category, string key)
            => GetImageApiPath(typeof(T).Name, category, key);

        private string GetImageVirtualPath(string directory, string category, string key)
            => $"/images/{directory}/{_hasher.FileNameHash(category, key)}.jpeg";

        public string GetImageVirtualPath<T>(string category, string key)
            => GetImageVirtualPath(typeof(T).Name, category, key);
    }

    public interface IPathProvider
    {
        string ApplicationPath { get; }
        string ImagesUploadDirectory { get; }

        string GetImagePhysicalPath<T>(string category, string key);

        string GetImageApiPath<T>(string category, string key);

        string GetImageVirtualPath<T>(string category, string key);
    }
}

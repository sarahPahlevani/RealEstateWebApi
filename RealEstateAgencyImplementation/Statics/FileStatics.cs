using System.Collections.Generic;

namespace RealEstateAgency.Shared.Statics
{
    public class FileStatics : IFileStatics
    {
        public HashSet<string> DocumentFileExtensions { get; } = new HashSet<string>
            {".doc",".docx",".odt",".txt",".pdf",".ppt",".pptx",".csv",".xlsx",".xls"};

        public HashSet<string> ImageFileExtensions { get; } = new HashSet<string>
            {".jpeg",".jpg",".png",".tiff",".tif",".gif"};

        public Dictionary<string, string> FileExtensionsContentType { get; } = new Dictionary<string, string>
        {
            {"doc", "application/msword"},
            {"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"odt", "application/vnd.oasis.opendocument.text"},
            {"txt", "text/plain"},
            {"pdf", "application/pdf"},
            {"ppt", "application/vnd.ms-powerpoint"},
            {"pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {"csv", "text/csv"},
            {"xlsx", "application/vnd.openxmlformats-officedocument.spreadshseetml.sheet"},
            {"xls", "application/vnd.ms-excel"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"png", "image/png"},
            {"tiff", "image/tiff"},
            {"tif", "image/tiff"},
            {"gif", "image/gif"},
        };
    }

    public interface IFileStatics
    {
        HashSet<string> ImageFileExtensions { get; }
        HashSet<string> DocumentFileExtensions { get; }
        Dictionary<string, string> FileExtensionsContentType { get; }
    }
}

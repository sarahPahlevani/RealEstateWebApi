namespace RealEstateAgency.Shared.BaseModels
{
    public class SerilogSetting : ISerilogSetting
    {
        public string LogFileName { get; set; }
        public string Path { get; set; }
        public int RollingInterval { get; set; }
    }

    public interface ISerilogSetting
    {
        string LogFileName { get; set; }
        string Path { get; set; }
        int RollingInterval { get; set; }
    }
}

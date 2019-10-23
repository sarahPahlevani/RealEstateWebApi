using System.Collections.Generic;

namespace RealEstateAgency.Shared.BaseModels
{
    public class CorsSetting : ICorsSetting
    {
        public string PolicyName { get; set; }
        public IEnumerable<string> AllowOrigins { get; set; }
    }

    public interface ICorsSetting
    {
        string PolicyName { get; set; }
        IEnumerable<string> AllowOrigins { get; set; }
    }
}

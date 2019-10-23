using System.Collections.Generic;

namespace RealEstateAgency.Shared.BaseModels
{
    public class AuthSetting : IAuthSetting
    {
        public string Secret { get; set; }
        public int ExpireInDays { get; set; }
    }

    public interface IAuthSetting
    {
        int ExpireInDays { get; set; }
        string Secret { get; set; }
    }
}

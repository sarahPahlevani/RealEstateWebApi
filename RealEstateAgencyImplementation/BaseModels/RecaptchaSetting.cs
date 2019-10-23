using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Shared.BaseModels
{
    public class RecaptchaSetting
    {
        public string SiteToken { get; set; }
        public string VerifyUrl { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Shared.BaseModels
{
    public class AppInformation
    {
        public string Version { get; set; }
        public string VersionType { get; set; }
        public string WebAppVersion { get; set; }
        public string WebAppVersionType { get; set; }
        public int BuildNumber { get; set; }
        public string DatabaseVersion { get; set; }
        public DateTime BuildDate { get; set; }
        public string AppName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AppSetting
    {
        public string DashboardBaseUrl { get; set; }
        public string ApiBaseUrl { get; set; }
        public string WebAppBaseUrl { get; set; }
        public bool DevelopmentMode { get; set; }
        public string AdminEmailAddress { get; set; }
        public AppInformation Info { get; set; }
    }
}

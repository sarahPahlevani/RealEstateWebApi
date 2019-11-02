using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;

namespace RealEstateAgency.Dtos.ModelDtos.SharingSystem
{
    public class UserSharedPropertyDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string PropertyType { get; set; }
        public string PropertyTitle { get; set; }
        public decimal PropertyPrice { get; set; }
        public PropertyWebAppImageDto PropertyImageId { get; set; }
        public string PropertyImageUrl { get; set; }
        public int? UserAccountId { get; set; }
        public int? SocialNetworkId { get; set; }
        public string SocialNetworkTitle { get; set; }
        public byte[] SocialNetworkIcon { get; set; }
        public string RefererUrl { get; set; }
        public int ClickCount { get; set; }
    }
}

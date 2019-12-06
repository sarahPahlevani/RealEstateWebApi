using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyFloorPlan
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public string FloorName { get; set; }
        public int? FloorPrice { get; set; }
        public string PricePostfix { get; set; }
        public int? FloorSize { get; set; }
        public string SizePostfix { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public string PlanDescription { get; set; }
        public string ImageCaption { get; set; }
        public DateTime? UploadDate { get; set; }
        public string ImageExtension { get; set; }
        public int? ImageSize { get; set; }
        public byte[] ImageContentFull1 { get; set; }
        public byte[] ImageContentTumblr1 { get; set; }
        public string ImagePath { get; set; }
        public string TumbPath { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
    }
}

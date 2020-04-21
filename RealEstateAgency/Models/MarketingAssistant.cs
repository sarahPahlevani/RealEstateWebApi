using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class MarketingAssistant
    {
        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public int UserAccountId { get; set; }
        public string MetadataJson { get; set; }
        public string Description { get; set; }
        public string TrackingCode { get; set; }
        public bool HasPlusLevel { get; set; }

        public virtual RealEstate RealEstate { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}

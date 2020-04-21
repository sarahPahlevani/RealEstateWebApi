using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PropertyDetail
    {
        public int Id { get; set; }
        public decimal? Size { get; set; }
        public decimal? LandArea { get; set; }
        public int? Rooms { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Garages { get; set; }
        public decimal? GaragesSize { get; set; }
        public int? YearBuild { get; set; }

        public virtual Property IdNavigation { get; set; }
    }
}

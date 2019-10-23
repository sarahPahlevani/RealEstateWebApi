using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyLocation
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public decimal? GoogleMapsLatitude { get; set; }
        public decimal? GoogleMapsLongitude { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Property IdNavigation { get; set; }
        public virtual Region Region { get; set; }
    }
}

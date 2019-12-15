using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyLocation
    {
        public int Id { get; set; }
        public int? CountryId1 { get; set; }
        public int? RegionId1 { get; set; }
        public int? CityId1 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public decimal? GoogleMapsLatitude { get; set; }
        public decimal? GoogleMapsLongitude { get; set; }

        public virtual City CityId1Navigation { get; set; }
        public virtual Country CountryId1Navigation { get; set; }
        public virtual Property IdNavigation { get; set; }
        public virtual Region RegionId1Navigation { get; set; }
    }
}

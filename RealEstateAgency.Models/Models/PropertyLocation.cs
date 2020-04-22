using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyLocation
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int? CityId { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GoogleMapsLatitude { get; set; }
        public string GoogleMapsLongitude { get; set; }

        public virtual City CityNavigation { get; set; }
        public virtual Property IdNavigation { get; set; }
    }
}

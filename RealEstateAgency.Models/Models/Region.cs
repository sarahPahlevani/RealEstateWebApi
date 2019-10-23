using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Region
    {
        public Region()
        {
            City = new HashSet<City>();
            PropertyLocation = new HashSet<PropertyLocation>();
            RegionTranslate = new HashSet<RegionTranslate>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<PropertyLocation> PropertyLocation { get; set; }
        public virtual ICollection<RegionTranslate> RegionTranslate { get; set; }
    }
}

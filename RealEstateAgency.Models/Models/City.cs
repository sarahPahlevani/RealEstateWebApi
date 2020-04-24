using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class City
    {
        public City()
        {
            CityTranslate = new HashSet<CityTranslate>();
            PropertyLocation = new HashSet<PropertyLocation>();
        }

        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<CityTranslate> CityTranslate { get; set; }
        public virtual ICollection<PropertyLocation> PropertyLocation { get; set; }
    }
}

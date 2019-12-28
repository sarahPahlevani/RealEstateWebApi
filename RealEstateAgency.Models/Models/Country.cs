using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Country
    {
        public Country()
        {
            CountryTranslate = new HashSet<CountryTranslate>();
            Region = new HashSet<Region>();
        }

        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string OfficialShortForm { get; set; }
        public string OfficialLongForm { get; set; }
        public int? Isocode { get; set; }
        public string Isoshort { get; set; }
        public string Isolong { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual ICollection<CountryTranslate> CountryTranslate { get; set; }
        public virtual ICollection<Region> Region { get; set; }
    }
}

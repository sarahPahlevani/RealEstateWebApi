using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Country = new HashSet<Country>();
            PropertyPrice = new HashSet<PropertyPrice>();
            RealEstate = new HashSet<RealEstate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<PropertyPrice> PropertyPrice { get; set; }
        public virtual ICollection<RealEstate> RealEstate { get; set; }
    }
}

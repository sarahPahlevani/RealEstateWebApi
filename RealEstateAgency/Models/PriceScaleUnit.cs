using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PriceScaleUnit
    {
        public PriceScaleUnit()
        {
            PriceScaleUnitTranslate = new HashSet<PriceScaleUnitTranslate>();
            PropertyPrice = new HashSet<PropertyPrice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Scale { get; set; }

        public virtual ICollection<PriceScaleUnitTranslate> PriceScaleUnitTranslate { get; set; }
        public virtual ICollection<PropertyPrice> PropertyPrice { get; set; }
    }
}

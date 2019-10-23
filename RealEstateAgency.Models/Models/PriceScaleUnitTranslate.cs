using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PriceScaleUnitTranslate
    {
        public int Id { get; set; }
        public int PriceScaleUnitId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual PriceScaleUnit PriceScaleUnit { get; set; }
    }
}

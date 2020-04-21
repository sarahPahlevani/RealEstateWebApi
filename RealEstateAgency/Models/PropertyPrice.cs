using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PropertyPrice
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int PriceScaleUnitId { get; set; }
        public decimal Price { get; set; }
        public string BeforePriceLabel { get; set; }
        public string AfterPriceLabel { get; set; }
        public bool PriceOnCall { get; set; }
        public decimal CalculatedPriceUnit { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Property IdNavigation { get; set; }
        public virtual PriceScaleUnit PriceScaleUnit { get; set; }
    }
}

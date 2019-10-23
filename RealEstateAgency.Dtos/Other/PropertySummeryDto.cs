using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.Other
{
    public class PropertySummeryDto
    {
        public int Id { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Title { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertyLabelId { get; set; }
        public int PropertyStatusId { get; set; }
        public int? CityId { get; set; }
        public int? RegionId { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int PriceScaleUnitId { get; set; }
        public decimal? Size { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Garages { get; set; }
        public int? Rooms { get; set; }
        public int? YearBuild { get; set; }
        public IEnumerable<PropertyImage> Images { get; set; }
        public List<string> ImagesUrl { get; set; }

    }
}

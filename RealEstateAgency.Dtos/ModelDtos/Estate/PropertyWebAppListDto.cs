using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyWebAppListDto
    {
        public int Id { get; set; }
        public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyLabel PropertyLabel { get; set; }
        public int PropertyLabelId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public int PropertyStatusId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public PropertyPrice Price { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public string PropertyUniqId { get; set; }
        public PropertyLocation PropertyLocation { get; set; }
        public PropertyDetail PropertyDetail { get; set; }
        public List<string> ImagesUrl { get; set; }
        public IEnumerable<PropertyWebAppImageDto> ImagesId { get; set; }
        public DateTime? PublishingDate { get; set; }
    }
}

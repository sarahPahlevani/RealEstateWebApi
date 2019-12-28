using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class WebSiteEstateDto : IDto
    {
        public int Id { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int PropertyLabelId { get; set; }
        public PropertyLabel PropertyLabel { get; set; }
        public int PropertyStatusId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public PropertyPrice Price { get; set; }
        public string PriceSymbol { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public string PropertyUniqId { get; set; }
        public PropertyLocation PropertyLocation { get; set; }
        public PropertyDetail PropertyDetail { get; set; }
        public List<string> ImagesUrl { get; set; }
        public int AgentId { get; set; }
        public DateTime? PublishingDate { get; set; }

        public IEnumerable<PropertyWebAppImageDto> Images { get; set; }
        public IEnumerable<PropertyAdditionalDetailDto> AdditionalDetails { get; set; }
        public IEnumerable<PropertyAttachmentDto> Attachments { get; set; }
        public IEnumerable<PropertyFloorPlanDto> FloorPlans { get; set; }
        public IEnumerable<int> FeatureIds { get; set; }

    }
}
using System;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.Other.PaginationListDtos
{
    public class PropertyPaginationListDto : IPaginationListDto
    {
        public int Id { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyLabelName { get; set; }
        public string PropertyStatusName { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public decimal Price { get; set; }
        public decimal CalculatedPriceUnit { get; set; }
        public string ZipCode { get; set; }
        public int CompletedSections { get; set; }
        public bool CheckReadyPublish { get; set; }
        public bool ReadyForPublish { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishingDate { get; set; }
        public UserAccount UserAccountReadyForPublish { get; set; }
        public UserAccount UserAccountPublished { get; set; }
        public DateTime? ReadyForPublishDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.CRM
{
    public class CommissionListDto : IDto
    {
        public int Id { get; set; }
        public byte CommissionPercent { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPay { get; set; }
        public string PayCode { get; set; }
        public DateTime? PayDate { get; set; }

        public int UserAccountId { get; set; }
        public string Username { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal PropertyPrice { get; set; }
        public Currency PropertyPriceCurrency { get; set; }
        
    }
}

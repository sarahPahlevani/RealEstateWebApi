using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.CRM
{
    public class CommissionInfoDto : IDto
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal Amount { get; set; }
        public bool IsPay { get; set; }

        public decimal TotalCommission { get; set; }
        public decimal TotalEarn { get; set; }
        

    }
}

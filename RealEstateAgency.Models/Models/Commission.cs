using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Commission
    {
        public int Id { get; set; }
        public byte CommissionPercent { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPay { get; set; }
        public string PayCode { get; set; }
        public DateTime? PayDate { get; set; }

        public virtual Request IdNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class Withdrawal
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}

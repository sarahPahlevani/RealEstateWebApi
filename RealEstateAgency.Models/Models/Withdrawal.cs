using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Withdrawal
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

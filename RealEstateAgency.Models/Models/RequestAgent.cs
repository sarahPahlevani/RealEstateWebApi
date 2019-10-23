using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class RequestAgent
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AgentId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Request Request { get; set; }
    }
}

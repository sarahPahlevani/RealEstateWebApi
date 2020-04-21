using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class RequestProperty
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int PropertyId { get; set; }
        public string Description { get; set; }

        public virtual Property Property { get; set; }
        public virtual Request Request { get; set; }
    }
}

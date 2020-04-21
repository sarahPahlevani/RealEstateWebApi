using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class SharedPropertyClick
    {
        public int Id { get; set; }
        public int SharedPropertyId { get; set; }
        public string MetaData { get; set; }

        public virtual SharedProperty SharedProperty { get; set; }
    }
}

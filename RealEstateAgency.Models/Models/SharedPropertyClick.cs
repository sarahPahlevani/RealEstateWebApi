using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class SharedPropertyClick
    {
        public SharedPropertyClick()
        {
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int SharedPropertyId { get; set; }
        public string MetaData { get; set; }

        public virtual SharedProperty SharedProperty { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}

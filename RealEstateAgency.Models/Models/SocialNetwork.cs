using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class SocialNetwork
    {
        public SocialNetwork()
        {
            Request = new HashSet<Request>();
            SharedProperty = new HashSet<SharedProperty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UniqueKey { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<SharedProperty> SharedProperty { get; set; }
    }
}

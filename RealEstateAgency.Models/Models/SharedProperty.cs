using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class SharedProperty
    {
        public SharedProperty()
        {
            SharedPropertyClick = new HashSet<SharedPropertyClick>();
        }

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int UserAccountId { get; set; }
        public int SocialNetworkId { get; set; }
        public string ReferralCodeAndSharingNetworkId { get; set; }

        public virtual Property Property { get; set; }
        public virtual SocialNetwork SocialNetwork { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<SharedPropertyClick> SharedPropertyClick { get; set; }
    }
}

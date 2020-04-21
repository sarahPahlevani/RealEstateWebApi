using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class UserAccountGroup
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int UserGroupId { get; set; }
        public int IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}

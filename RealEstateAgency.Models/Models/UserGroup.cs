using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            UserAccountGroup = new HashSet<UserAccountGroup>();
            UserGroupTranslate = new HashSet<UserGroupTranslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StaticCode { get; set; }

        public virtual ICollection<UserAccountGroup> UserAccountGroup { get; set; }
        public virtual ICollection<UserGroupTranslate> UserGroupTranslate { get; set; }
    }
}

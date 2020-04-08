using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class UserGroupPermission
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public int MenuId { get; set; }
        public bool DeletePermission { get; set; }
        public bool UpdatePermission { get; set; }
        public bool ReadPermission { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}

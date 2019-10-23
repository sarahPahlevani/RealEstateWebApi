using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class UserAccountWishList
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Bookmark
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int PropertyId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}

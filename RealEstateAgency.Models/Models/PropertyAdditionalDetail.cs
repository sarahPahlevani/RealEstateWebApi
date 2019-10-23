using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyAdditionalDetail
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyLabel
    {
        public PropertyLabel()
        {
            Property = new HashSet<Property>();
            PropertyLabelTranslate = new HashSet<PropertyLabelTranslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<PropertyLabelTranslate> PropertyLabelTranslate { get; set; }
    }
}

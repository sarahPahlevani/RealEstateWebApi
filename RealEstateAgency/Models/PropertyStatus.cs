using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PropertyStatus
    {
        public PropertyStatus()
        {
            Property = new HashSet<Property>();
            PropertyStatusTranslate = new HashSet<PropertyStatusTranslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<PropertyStatusTranslate> PropertyStatusTranslate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Property = new HashSet<Property>();
            PropertyTypeTranslate = new HashSet<PropertyTypeTranslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool Deleted { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<PropertyTypeTranslate> PropertyTypeTranslate { get; set; }
    }
}

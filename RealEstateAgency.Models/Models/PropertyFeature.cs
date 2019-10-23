using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyFeature
    {
        public PropertyFeature()
        {
            PropertyFeatureTranslate = new HashSet<PropertyFeatureTranslate>();
            PropertyInvolveFeature = new HashSet<PropertyInvolveFeature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool Deleted { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<PropertyFeatureTranslate> PropertyFeatureTranslate { get; set; }
        public virtual ICollection<PropertyInvolveFeature> PropertyInvolveFeature { get; set; }
    }
}

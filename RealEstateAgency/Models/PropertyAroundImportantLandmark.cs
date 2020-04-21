using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PropertyAroundImportantLandmark
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ImportantPlaceTypeId { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public string Description { get; set; }
        public decimal? DistanceMiles { get; set; }
        public decimal? DistanceKm { get; set; }
        public int? WalkToMin { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ImportantPlaceType ImportantPlaceType { get; set; }
        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
    }
}

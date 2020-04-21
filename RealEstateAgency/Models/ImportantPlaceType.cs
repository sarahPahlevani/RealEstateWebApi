using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class ImportantPlaceType
    {
        public ImportantPlaceType()
        {
            ImportantPlaceTypeTranslate = new HashSet<ImportantPlaceTypeTranslate>();
            PropertyAroundImportantLandmark = new HashSet<PropertyAroundImportantLandmark>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] TypeIcon { get; set; }
        public bool Deleted { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<ImportantPlaceTypeTranslate> ImportantPlaceTypeTranslate { get; set; }
        public virtual ICollection<PropertyAroundImportantLandmark> PropertyAroundImportantLandmark { get; set; }
    }
}

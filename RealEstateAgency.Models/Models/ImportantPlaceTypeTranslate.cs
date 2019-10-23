using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class ImportantPlaceTypeTranslate
    {
        public int Id { get; set; }
        public int ImportantPlaceTypeId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ImportantPlaceType ImportantPlaceType { get; set; }
        public virtual Language Language { get; set; }
    }
}

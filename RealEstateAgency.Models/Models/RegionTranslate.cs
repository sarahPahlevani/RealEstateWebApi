using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class RegionTranslate
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual Region Region { get; set; }
    }
}

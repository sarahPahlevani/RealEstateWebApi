using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class CityTranslate
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
        public virtual Language Language { get; set; }
    }
}

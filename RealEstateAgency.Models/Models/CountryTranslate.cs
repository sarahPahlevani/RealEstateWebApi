using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class CountryTranslate
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual Language Language { get; set; }
    }
}

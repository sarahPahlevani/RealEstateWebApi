using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class ContentPageTranslate
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int? ComtentPageId { get; set; }
        public string ContentHeader { get; set; }
        public string ContectDetail { get; set; }
        public string ContentFooter { get; set; }

        public virtual ContentPage ComtentPage { get; set; }
        public virtual Language Language { get; set; }
    }
}

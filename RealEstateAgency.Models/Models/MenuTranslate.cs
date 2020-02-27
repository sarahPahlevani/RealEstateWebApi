using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class MenuTranslate
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual Menu Menu { get; set; }
    }
}

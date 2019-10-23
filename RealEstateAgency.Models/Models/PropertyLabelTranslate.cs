using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyLabelTranslate
    {
        public int Id { get; set; }
        public int PropertyLabelId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual PropertyLabel PropertyLabel { get; set; }
    }
}

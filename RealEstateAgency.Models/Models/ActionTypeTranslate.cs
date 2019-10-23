using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class ActionTypeTranslate
    {
        public int Id { get; set; }
        public int ActionTypeId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual Language Language { get; set; }
    }
}

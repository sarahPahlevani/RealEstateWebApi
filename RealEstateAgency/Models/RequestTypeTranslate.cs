using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class RequestTypeTranslate
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual RequestType RequestType { get; set; }
    }
}

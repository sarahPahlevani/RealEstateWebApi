using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class UserGroupTranslate
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class PropertyFeatureTranslate
    {
        public int Id { get; set; }
        public int PropertyFeatureId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Language Language { get; set; }
        public virtual PropertyFeature PropertyFeature { get; set; }
    }
}

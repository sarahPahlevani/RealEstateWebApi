﻿using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class Subscribes
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime? InsertDateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Apicontroller
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public int? MenuId { get; set; }
        public bool AllAccess { get; set; }
        public string ActionName { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsRead { get; set; }

        public virtual Menu Menu { get; set; }
    }
}

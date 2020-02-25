using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
            UserGroupPermission = new HashSet<UserGroupPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPanelPage { get; set; }
        public int? ParentId { get; set; }
        public string PluginName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
        public virtual ICollection<UserGroupPermission> UserGroupPermission { get; set; }
    }
}

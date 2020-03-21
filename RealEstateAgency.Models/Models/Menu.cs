using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Menu
    {
        public Menu()
        {
            ContentPage = new HashSet<ContentPage>();
            InverseParent = new HashSet<Menu>();
            MenuTranslate = new HashSet<MenuTranslate>();
            UserGroupPermission = new HashSet<UserGroupPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPanelPage { get; set; }
        public int? ParentId { get; set; }
        public string PluginName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconName { get; set; }

        public virtual Menu Parent { get; set; }
        public virtual ICollection<ContentPage> ContentPage { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
        public virtual ICollection<MenuTranslate> MenuTranslate { get; set; }
        public virtual ICollection<UserGroupPermission> UserGroupPermission { get; set; }
    }
}

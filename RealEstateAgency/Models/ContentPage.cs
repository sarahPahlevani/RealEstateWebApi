using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class ContentPage
    {
        public ContentPage()
        {
            ContentPageTranslate = new HashSet<ContentPageTranslate>();
        }

        public int Id { get; set; }
        public string ContentHeader { get; set; }
        public string ContectDetail { get; set; }
        public string ContentFooter { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<ContentPageTranslate> ContentPageTranslate { get; set; }
    }
}

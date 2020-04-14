using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class RealEstate
    {
        public RealEstate()
        {
            Agent = new HashSet<Agent>();
            MarketingAssistant = new HashSet<MarketingAssistant>();
        }

        public int Id { get; set; }
        public int LanguageIdDefault { get; set; }
        public string Name { get; set; }
        public string LogoPicture { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Phone03 { get; set; }
        public string Fax { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string Domain { get; set; }
        public string MetadataJson { get; set; }
        public string DateFormat { get; set; }
        public int CurrencyId { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Language LanguageIdDefaultNavigation { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
        public virtual ICollection<MarketingAssistant> MarketingAssistant { get; set; }
    }
}

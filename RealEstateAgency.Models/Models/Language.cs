using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Language
    {
        public Language()
        {
            ActionTypeTranslate = new HashSet<ActionTypeTranslate>();
            CityTranslate = new HashSet<CityTranslate>();
            CountryTranslate = new HashSet<CountryTranslate>();
            ImportantPlaceTypeTranslate = new HashSet<ImportantPlaceTypeTranslate>();
            MenuTranslate = new HashSet<MenuTranslate>();
            PriceScaleUnitTranslate = new HashSet<PriceScaleUnitTranslate>();
            PropertyFeatureTranslate = new HashSet<PropertyFeatureTranslate>();
            PropertyLabelTranslate = new HashSet<PropertyLabelTranslate>();
            PropertyStatusTranslate = new HashSet<PropertyStatusTranslate>();
            PropertyTypeTranslate = new HashSet<PropertyTypeTranslate>();
            RealEstate = new HashSet<RealEstate>();
            RegionTranslate = new HashSet<RegionTranslate>();
            RequestTypeTranslate = new HashSet<RequestTypeTranslate>();
            UserGroupTranslate = new HashSet<UserGroupTranslate>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public bool IsDefault { get; set; }

        public virtual ContentPageTranslate ContentPageTranslate { get; set; }
        public virtual ICollection<ActionTypeTranslate> ActionTypeTranslate { get; set; }
        public virtual ICollection<CityTranslate> CityTranslate { get; set; }
        public virtual ICollection<CountryTranslate> CountryTranslate { get; set; }
        public virtual ICollection<ImportantPlaceTypeTranslate> ImportantPlaceTypeTranslate { get; set; }
        public virtual ICollection<MenuTranslate> MenuTranslate { get; set; }
        public virtual ICollection<PriceScaleUnitTranslate> PriceScaleUnitTranslate { get; set; }
        public virtual ICollection<PropertyFeatureTranslate> PropertyFeatureTranslate { get; set; }
        public virtual ICollection<PropertyLabelTranslate> PropertyLabelTranslate { get; set; }
        public virtual ICollection<PropertyStatusTranslate> PropertyStatusTranslate { get; set; }
        public virtual ICollection<PropertyTypeTranslate> PropertyTypeTranslate { get; set; }
        public virtual ICollection<RealEstate> RealEstate { get; set; }
        public virtual ICollection<RegionTranslate> RegionTranslate { get; set; }
        public virtual ICollection<RequestTypeTranslate> RequestTypeTranslate { get; set; }
        public virtual ICollection<UserGroupTranslate> UserGroupTranslate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Property
    {
        public Property()
        {
            PropertyAdditionalDetail = new HashSet<PropertyAdditionalDetail>();
            PropertyAroundImportantLandmark = new HashSet<PropertyAroundImportantLandmark>();
            PropertyAttachment = new HashSet<PropertyAttachment>();
            PropertyFloorPlan = new HashSet<PropertyFloorPlan>();
            PropertyImage = new HashSet<PropertyImage>();
            PropertyInvolveFeature = new HashSet<PropertyInvolveFeature>();
            RequestProperty = new HashSet<RequestProperty>();
            SharedProperty = new HashSet<SharedProperty>();
            UserAccountWishList = new HashSet<UserAccountWishList>();
        }

        public int Id { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertyLabelId { get; set; }
        public int PropertyStatusId { get; set; }
        public int? RequestId { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public string Title { get; set; }
        public string PropertyUniqId { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string DraftInformation { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool ReadyForPublish { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishingDate { get; set; }
        public int? UserAccountIdReadyForPublish { get; set; }
        public int? UserAccountIdPublished { get; set; }
        public DateTime? ReadyForPublishDate { get; set; }
        public byte? Commission { get; set; }

        public virtual PropertyLabel PropertyLabel { get; set; }
        public virtual PropertyStatus PropertyStatus { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual Request Request { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual UserAccount UserAccountIdPublishedNavigation { get; set; }
        public virtual UserAccount UserAccountIdReadyForPublishNavigation { get; set; }
        public virtual PropertyDetail PropertyDetail { get; set; }
        public virtual PropertyLocation PropertyLocation { get; set; }
        public virtual PropertyPrice PropertyPrice { get; set; }
        public virtual ICollection<PropertyAdditionalDetail> PropertyAdditionalDetail { get; set; }
        public virtual ICollection<PropertyAroundImportantLandmark> PropertyAroundImportantLandmark { get; set; }
        public virtual ICollection<PropertyAttachment> PropertyAttachment { get; set; }
        public virtual ICollection<PropertyFloorPlan> PropertyFloorPlan { get; set; }
        public virtual ICollection<PropertyImage> PropertyImage { get; set; }
        public virtual ICollection<PropertyInvolveFeature> PropertyInvolveFeature { get; set; }
        public virtual ICollection<RequestProperty> RequestProperty { get; set; }
        public virtual ICollection<SharedProperty> SharedProperty { get; set; }
        public virtual ICollection<UserAccountWishList> UserAccountWishList { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            AgentUserAccount = new HashSet<Agent>();
            AgentUserAccountIdDeleteByNavigation = new HashSet<Agent>();
            ImportantPlaceType = new HashSet<ImportantPlaceType>();
            MarketingAssistant = new HashSet<MarketingAssistant>();
            PropertyAdditionalDetail = new HashSet<PropertyAdditionalDetail>();
            PropertyAroundImportantLandmark = new HashSet<PropertyAroundImportantLandmark>();
            PropertyAttachment = new HashSet<PropertyAttachment>();
            PropertyFeature = new HashSet<PropertyFeature>();
            PropertyFloorPlan = new HashSet<PropertyFloorPlan>();
            PropertyImage = new HashSet<PropertyImage>();
            PropertyInvolveFeature = new HashSet<PropertyInvolveFeature>();
            PropertyLabel = new HashSet<PropertyLabel>();
            PropertyStatus = new HashSet<PropertyStatus>();
            PropertyType = new HashSet<PropertyType>();
            PropertyUserAccountIdDeleteByNavigation = new HashSet<Property>();
            PropertyUserAccountIdPublishedNavigation = new HashSet<Property>();
            PropertyUserAccountIdReadyForPublishNavigation = new HashSet<Property>();
            RealEstate = new HashSet<RealEstate>();
            RequestUserAccountIdDeleteByNavigation = new HashSet<Request>();
            RequestUserAccountIdRequesterNavigation = new HashSet<Request>();
            RequestUserAccountIdSharedNavigation = new HashSet<Request>();
            SharedProperty = new HashSet<SharedProperty>();
            UserAccountGroup = new HashSet<UserAccountGroup>();
            UserAccountWishList = new HashSet<UserAccountWishList>();
        }

        public int Id { get; set; }
        public int? AuthenticationProviderId { get; set; }
        public string AuthenticationProviderAccessToken { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool? IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasExternalAuthentication { get; set; }
        public string ActivationKey { get; set; }
        public string ResetPasswordKey { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string ZipCode { get; set; }
        public byte[] UserPicture { get; set; }
        public byte[] UserPictureTumblr { get; set; }
        public string ReferralCode { get; set; }

        public virtual AuthenticationProvider AuthenticationProvider { get; set; }
        public virtual ICollection<Agent> AgentUserAccount { get; set; }
        public virtual ICollection<Agent> AgentUserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<ImportantPlaceType> ImportantPlaceType { get; set; }
        public virtual ICollection<MarketingAssistant> MarketingAssistant { get; set; }
        public virtual ICollection<PropertyAdditionalDetail> PropertyAdditionalDetail { get; set; }
        public virtual ICollection<PropertyAroundImportantLandmark> PropertyAroundImportantLandmark { get; set; }
        public virtual ICollection<PropertyAttachment> PropertyAttachment { get; set; }
        public virtual ICollection<PropertyFeature> PropertyFeature { get; set; }
        public virtual ICollection<PropertyFloorPlan> PropertyFloorPlan { get; set; }
        public virtual ICollection<PropertyImage> PropertyImage { get; set; }
        public virtual ICollection<PropertyInvolveFeature> PropertyInvolveFeature { get; set; }
        public virtual ICollection<PropertyLabel> PropertyLabel { get; set; }
        public virtual ICollection<PropertyStatus> PropertyStatus { get; set; }
        public virtual ICollection<PropertyType> PropertyType { get; set; }
        public virtual ICollection<Property> PropertyUserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Property> PropertyUserAccountIdPublishedNavigation { get; set; }
        public virtual ICollection<Property> PropertyUserAccountIdReadyForPublishNavigation { get; set; }
        public virtual ICollection<RealEstate> RealEstate { get; set; }
        public virtual ICollection<Request> RequestUserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Request> RequestUserAccountIdRequesterNavigation { get; set; }
        public virtual ICollection<Request> RequestUserAccountIdSharedNavigation { get; set; }
        public virtual ICollection<SharedProperty> SharedProperty { get; set; }
        public virtual ICollection<UserAccountGroup> UserAccountGroup { get; set; }
        public virtual ICollection<UserAccountWishList> UserAccountWishList { get; set; }
    }
}

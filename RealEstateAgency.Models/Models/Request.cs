using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Request
    {
        public Request()
        {
            Property = new HashSet<Property>();
            RequestAction = new HashSet<RequestAction>();
            RequestAgent = new HashSet<RequestAgent>();
            RequestProperty = new HashSet<RequestProperty>();
            RequestState = new HashSet<RequestState>();
        }

        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public int UserAccountIdRequester { get; set; }
        public int? WorkflowId { get; set; }
        public string TrackingNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MarketingAssistantTrackingCode { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? SharedPropertyClickId { get; set; }
        public int? PropertyId { get; set; }

        public virtual RequestType RequestType { get; set; }
        public virtual SharedPropertyClick SharedPropertyClick { get; set; }
        public virtual UserAccount UserAccountIdRequesterNavigation { get; set; }
        public virtual Workflow Workflow { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<RequestAction> RequestAction { get; set; }
        public virtual ICollection<RequestAgent> RequestAgent { get; set; }
        public virtual ICollection<RequestProperty> RequestProperty { get; set; }
        public virtual ICollection<RequestState> RequestState { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class RequestAction
    {
        public RequestAction()
        {
            RequestActionFollowUp = new HashSet<RequestActionFollowUp>();
        }

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ActionTypeId { get; set; }
        public int AgentId { get; set; }
        public int? RequestActionFollowUpReference { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionTime { get; set; }
        public bool? ActionIsSuccess { get; set; }
        public string MetaDataJson { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Request Request { get; set; }
        public virtual RequestActionFollowUp RequestActionFollowUpReferenceNavigation { get; set; }
        public virtual ICollection<RequestActionFollowUp> RequestActionFollowUp { get; set; }
    }
}

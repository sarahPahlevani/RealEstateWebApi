using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class RequestActionFollowUp
    {
        public RequestActionFollowUp()
        {
            RequestAction = new HashSet<RequestAction>();
        }

        public int Id { get; set; }
        public int RequestActionId { get; set; }
        public int ActionTypeId { get; set; }
        public int AgentIdFollowUp { get; set; }
        public string Description { get; set; }
        public DateTime FollowUpDateTime { get; set; }
        public DateTime? FollowUpDateTimeSnooze { get; set; }
        public bool IsDone { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual Agent AgentIdFollowUpNavigation { get; set; }
        public virtual RequestAction RequestActionNavigation { get; set; }
        public virtual ICollection<RequestAction> RequestAction { get; set; }
    }
}

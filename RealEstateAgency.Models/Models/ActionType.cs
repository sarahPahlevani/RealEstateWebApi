using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class ActionType
    {
        public ActionType()
        {
            ActionTypeTranslate = new HashSet<ActionTypeTranslate>();
            RequestAction = new HashSet<RequestAction>();
            RequestActionFollowUp = new HashSet<RequestActionFollowUp>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ActionTypeTranslate> ActionTypeTranslate { get; set; }
        public virtual ICollection<RequestAction> RequestAction { get; set; }
        public virtual ICollection<RequestActionFollowUp> RequestActionFollowUp { get; set; }
    }
}

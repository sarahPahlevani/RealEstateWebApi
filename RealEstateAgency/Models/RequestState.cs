using System;
using System.Collections.Generic;

namespace RealEstateAgency.Models
{
    public partial class RequestState
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int WorkflowStepId { get; set; }
        public DateTime? StartStepDate { get; set; }
        public string Description { get; set; }
        public DateTime? FinishedDate { get; set; }
        public bool IsDone { get; set; }
        public int AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Request Request { get; set; }
        public virtual WorkflowStep WorkflowStep { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class WorkflowStep
    {
        public WorkflowStep()
        {
            RequestState = new HashSet<RequestState>();
        }

        public int Id { get; set; }
        public int WorkflowId { get; set; }
        public string Name { get; set; }
        public int StepNumber { get; set; }
        public bool IsFinish { get; set; }

        public virtual Workflow Workflow { get; set; }
        public virtual ICollection<RequestState> RequestState { get; set; }
    }
}

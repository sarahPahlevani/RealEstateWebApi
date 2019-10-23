using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Workflow
    {
        public Workflow()
        {
            Request = new HashSet<Request>();
            WorkflowStep = new HashSet<WorkflowStep>();
        }

        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public string Name { get; set; }

        public virtual RequestType RequestType { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<WorkflowStep> WorkflowStep { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class RequestType
    {
        public RequestType()
        {
            Request = new HashSet<Request>();
            RequestTypeTranslate = new HashSet<RequestTypeTranslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanAddProperty { get; set; }

        public virtual Workflow Workflow { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<RequestTypeTranslate> RequestTypeTranslate { get; set; }
    }
}

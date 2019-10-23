using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class ChangeRequestAgentDto
    {
        public int NewAgentId { get; set; }
        public int RequestId { get; set; }
        public string Description { get; set; }
    }
}

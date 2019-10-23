using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestActionListDto : IDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ActionTypeId { get; set; }
        public int AgentId { get; set; }
        public int? RequestActionFollowUpReference { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionTime { get; set; }
        public bool? ActionIsSuccess { get; set; }
        public Request Request { get; set; }
        public IEnumerable<RequestActionFollowUp> ActionFollowUps { get; set; }
        public ActionType ActionType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestStateListDto : IDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int WorkflowStepId { get; set; }
        public WorkflowStep WorkflowStep { get; set; }
        public DateTime? StartStepDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public Agent Agent { get; set; }
    }
}

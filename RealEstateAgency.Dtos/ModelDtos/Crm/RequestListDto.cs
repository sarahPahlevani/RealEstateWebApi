using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestListDto : IDto
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        //public RequestType RequestType { get; set; }
        public string RequestTypeName { get; set; }
        public int? WorkflowId { get; set; }
        //public Workflow Workflow { get; set; }
        //public string WorkflowStepName { get; set; }
        public bool CanAddProperty { get; set; }
        public int? UserAccountIdShared { get; set; }
        //public UserAccount UserAccountShared { get; set; }
        public string UserAccountSharedFullname { get; set; }
        public int? NetworkIdShared { get; set; }
        //public SocialNetwork NetworkShared { get; set; }
        public string NetworkSharedName { get; set; }
        public int? UserAccountIdRequester { get; set; }
        public string RequesterFullname { get; set; }
        public string RequesterEmail { get; set; }
        public string RequesterPhone { get; set; }
        public string TrackingNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public UserAccount User { get; set; }
        public int? AgentId { get; set; }
        public string AgentName { get; set; }
        //public Property Property { get; set; }
        public int? PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        //public IEnumerable<RequestAction> Actions { get; set; }
        //public IEnumerable<RequestState> States { get; set; }
        public string LatestStateName { get; set; }
        public bool? LatestStateIsFinish { get; set; }
        public bool IsAssigned { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDone { get; set; }
        public bool IsSuccess { get; set; }
        public byte? Commission { get; set; }
    }
}

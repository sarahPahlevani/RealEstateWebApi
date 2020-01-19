using System;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestDto : ModelDtoBase<Request>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestTypeId { get; set; }

        public RequestType RequestType { get; set; }

        public int? WorkflowId { get; set; }

        public Workflow Workflow { get; set; }

        public bool CanAddProperty { get; set; }

        public int? UserAccountIdShared { get; set; }

        public UserAccount UserAccountShared { get; set; }

        public int? NetworkIdShared { get; set; }

        public SocialNetwork NetworkShared { get; set; }

        public int? UserAccountIdRequester { get; set; }

        public string RequesterFullname { get; set; }

        public string RequesterEmail { get; set; }

        public string RequesterPhone { get; set; }

        public string TrackingNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public UserAccount User { get; set; }
        public int? AgentId { get; set; }
        public Agent Agent { get; set; }

        
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }

        public int? PropertyId { get; set; }

        public byte? Commission { get; set; }

        public Property Property { get; set; }
        public IEnumerable<RequestAction> Actions { get; set; }
        public IEnumerable<RequestState> States { get; set; }
        public bool IsAssigned { get; set; }

        public bool? IsDone { get; set; }

        public override IModelDto<Request> From(Request entity)
        {
            Id = entity.Id;
            RequestTypeId = entity.RequestTypeId;
            RequestType = entity.RequestType;
            AgentId = entity.AgentId;
            Agent = entity.Agent;
            WorkflowId = entity.WorkflowId;
            Workflow = entity.Workflow;
            UserAccountIdShared = entity.UserAccountIdShared;
            UserAccountShared = entity.UserAccountIdSharedNavigation;
            NetworkIdShared = entity.NetworkIdShared;
            NetworkShared = entity.NetworkIdSharedNavigation;
            UserAccountIdRequester = entity.UserAccountIdRequester;
            RequesterFullname = RequesterFullname;
            RequesterEmail = RequesterEmail;
            RequesterPhone = RequesterPhone;
            TrackingNumber = entity.TrackingNumber;
            Title = entity.Title;
            Description = entity.Description;
            User = entity.UserAccountIdRequesterNavigation;
            DateCreated = entity.DateCreated;
            Deleted = entity.Deleted;
            DeletedDate = entity.DeletedDate;
            UserAccountIdDeleteBy = entity.UserAccountIdDeleteBy;
            PropertyId = entity.PropertyId;
            Property = entity.Property.FirstOrDefault(r => r.RequestId == entity.Id);
            States = entity.RequestState;
            Actions = entity.RequestAction;
            IsDone = entity.IsDone;
            return this;
        }

        public override Request Create() =>
            new Request
            {
                RequestTypeId = RequestTypeId,
                UserAccountIdShared = UserAccountIdShared,
                NetworkIdShared = NetworkIdShared,
                UserAccountIdRequester = UserAccountIdRequester,
                RequesterFullname = RequesterFullname,
                RequesterEmail = RequesterEmail,
                RequesterPhone = RequesterPhone,
                WorkflowId = WorkflowId,
                TrackingNumber = TrackingNumber,
                Title = Title,
                Description = Description,
                DateCreated = DateCreated,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                UserAccountIdDeleteBy = UserAccountIdDeleteBy,
                PropertyId = PropertyId,
                AgentId = AgentId,
                IsDone = IsDone,
            };

        public override Request Update() =>
            new Request
            {
                Id = Id,
                RequestTypeId = RequestTypeId,
                UserAccountIdShared = UserAccountIdShared,
                NetworkIdShared = NetworkIdShared,
                UserAccountIdRequester = UserAccountIdRequester,
                RequesterFullname = RequesterFullname,
                RequesterEmail = RequesterEmail,
                RequesterPhone = RequesterPhone,
                WorkflowId = WorkflowId,
                TrackingNumber = TrackingNumber,
                Title = Title,
                Description = Description,
                DateCreated = DateCreated,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                UserAccountIdDeleteBy = UserAccountIdDeleteBy,
                PropertyId = PropertyId,
                AgentId = AgentId,
                IsDone = IsDone,
            };
    }
}

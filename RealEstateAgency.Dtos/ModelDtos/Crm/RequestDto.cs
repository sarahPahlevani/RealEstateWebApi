using System;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestDto : ModelDtoBase<Request>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestTypeId { get; set; }

        [Required]
        public int UserAccountIdRequester { get; set; }

        public int? WorkflowId { get; set; }

        public string TrackingNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string MarketingAssistantTrackingCode { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public int? SharedPropertyClickId { get; set; }

        public override IModelDto<Request> From(Request entity)
        {
            Id = entity.Id;
            RequestTypeId = entity.RequestTypeId;
            UserAccountIdRequester = entity.UserAccountIdRequester;
            WorkflowId = entity.WorkflowId;
            TrackingNumber = entity.TrackingNumber;
            Title = entity.Title;
            Description = entity.Description;
            MarketingAssistantTrackingCode = entity.MarketingAssistantTrackingCode;
            DateCreated = entity.DateCreated;
            Deleted = entity.Deleted;
            DeletedDate = entity.DeletedDate;
            UserAccountIdDeleteBy = entity.UserAccountIdDeleteBy;
            SharedPropertyClickId = entity.SharedPropertyClickId;
            return this;
        }

        public override Request Create() =>
            new Request
            {
                RequestTypeId = RequestTypeId,
                UserAccountIdRequester = UserAccountIdRequester,
                WorkflowId = WorkflowId,
                TrackingNumber = TrackingNumber,
                Title = Title,
                Description = Description,
                MarketingAssistantTrackingCode = MarketingAssistantTrackingCode,
                DateCreated = DateCreated,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                UserAccountIdDeleteBy = UserAccountIdDeleteBy,
                SharedPropertyClickId = SharedPropertyClickId
            };

        public override Request Update() =>
            new Request
            {
                Id = Id,
                RequestTypeId = RequestTypeId,
                UserAccountIdRequester = UserAccountIdRequester,
                WorkflowId = WorkflowId,
                TrackingNumber = TrackingNumber,
                Title = Title,
                Description = Description,
                MarketingAssistantTrackingCode = MarketingAssistantTrackingCode,
                DateCreated = DateCreated,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                UserAccountIdDeleteBy = UserAccountIdDeleteBy,
                SharedPropertyClickId = SharedPropertyClickId
            };
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyDto : ModelDtoBase<Property>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyTypeId { get; set; }

        [Required]
        public int PropertyLabelId { get; set; }

        [Required]
        public int PropertyStatusId { get; set; }

        public string Title { get; set; }

        public string PropertyUniqId { get; set; }

        [Required]
        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string DraftInformation { get; set; }

        public string ExtraInformation { get; set; }

        public DateTime? DateUpdated { get; set; }
        public int? RequestId { get; set; }
        public bool ReadyForPublish { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishingDate { get; set; }
        public int? UserAccountIdReadyForPublish { get; set; }
        public int? UserAccountIdPublished { get; set; }
        public DateTime? ReadyForPublishDate { get; set; }

        public override IModelDto<Property> From(Property entity)
        {
            Id = entity.Id;
            PropertyTypeId = entity.PropertyTypeId;
            PropertyLabelId = entity.PropertyLabelId;
            PropertyStatusId = entity.PropertyStatusId;
            Title = entity.Title;
            PropertyUniqId = entity.PropertyUniqId;
            Description = entity.Description;
            VideoUrl = entity.VideoUrl;
            DateUpdated = entity.DateUpdated;
            RequestId = entity.RequestId;
            ReadyForPublish = entity.ReadyForPublish;
            PublishingDate = entity.PublishingDate;
            IsPublished = entity.IsPublished;
            UserAccountIdReadyForPublish = entity.UserAccountIdReadyForPublish;
            UserAccountIdPublished = entity.UserAccountIdPublished;
            ReadyForPublishDate = entity.ReadyForPublishDate;
            ExtraInformation = entity.ExtraInformation;
            DraftInformation = entity.DraftInformation;
            return this;
        }

        public override Property Create() =>
            new Property
            {
                PropertyTypeId = PropertyTypeId,
                PropertyLabelId = PropertyLabelId,
                PropertyStatusId = PropertyStatusId,
                Title = string.IsNullOrWhiteSpace(Title)? "" : Title,
                PropertyUniqId = PropertyUniqId,
                Description = Description,
                VideoUrl = VideoUrl,
                DateUpdated = DateTime.Now,
                RequestId = RequestId,
                PublishingDate = PublishingDate,
                IsPublished = IsPublished,
                ReadyForPublish = ReadyForPublish,
                UserAccountIdReadyForPublish = UserAccountIdReadyForPublish,
                UserAccountIdPublished = UserAccountIdPublished,
                ReadyForPublishDate = ReadyForPublishDate,
                ExtraInformation = ExtraInformation,
                DraftInformation = DraftInformation
            };

        public override Property Update() =>
            new Property
            {
                Id = Id,
                PropertyTypeId = PropertyTypeId,
                PropertyLabelId = PropertyLabelId,
                PropertyStatusId = PropertyStatusId,
                Title = string.IsNullOrWhiteSpace(Title) ? "" : Title,
                PropertyUniqId = PropertyUniqId,
                Description = Description,
                VideoUrl = VideoUrl,
                DateUpdated = DateTime.Now,
                RequestId = RequestId,
                PublishingDate = PublishingDate,
                IsPublished = IsPublished,
                ReadyForPublish = ReadyForPublish,
                UserAccountIdReadyForPublish = UserAccountIdReadyForPublish,
                UserAccountIdPublished = UserAccountIdPublished,
                ReadyForPublishDate = ReadyForPublishDate,
                ExtraInformation = ExtraInformation,
                DraftInformation = DraftInformation
            };
    }
}

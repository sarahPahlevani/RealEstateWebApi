using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyAttachmentDto : ModelDtoBase<PropertyAttachment>
    {
        public override int Id { get; set; }
        public int PropertyId { get; set; }
        public string FileCaption { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileExtension { get; set; }
        public int FileSize { get; set; }

        public override IModelDto<PropertyAttachment> From(PropertyAttachment entity)
        {
            Id = entity.Id;
            PropertyId = entity.PropertyId;
            FileCaption = entity.FileCaption;
            UploadDate = entity.UploadDate;
            FileExtension = entity.FileExtension;
            FileSize = entity.FileSize;
            return this;
        }

        public override PropertyAttachment Create()
        {
            throw new NotImplementedException();
        }

        public override PropertyAttachment Update()
        {
            throw new NotImplementedException();
        }
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyImageDto : ModelDtoBase<PropertyImage>
    {
        public override int Id { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyId { get; set; }
        public string ImageCaption { get; set; }
        public DateTime UploadDate { get; set; }
        public string ImageExtension { get; set; }
        public string ImagePath { get; set; }
        public string TumbPath { get; set; }
        public int ImageSize { get; set; }
        public bool Is360View { get; set; }
        public int Priority { get; set; }
        public bool IsForBanner { get; set; }

        public override IModelDto<PropertyImage> From(PropertyImage entity)
        {
            Id = entity.Id;
            ImagePath = entity.ImagePath;
            TumbPath = entity.TumbPath;
            return this;
        }

        public override PropertyImage Create()
        {
            throw new NotImplementedException();
        }

        public override PropertyImage Update()
        {
            throw new NotImplementedException();
        }
    }
}

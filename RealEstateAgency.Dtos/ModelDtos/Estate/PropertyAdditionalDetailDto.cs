using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyAdditionalDetailDto : ModelDtoBase<PropertyAdditionalDetail>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Value { get; set; }

        public override IModelDto<PropertyAdditionalDetail> From(PropertyAdditionalDetail entity)
        {
            Id = entity.Id;
            PropertyId = entity.PropertyId;
            Title = entity.Title;
            Value = entity.Value;
            return this;
        }

        public override PropertyAdditionalDetail Create() =>
            new PropertyAdditionalDetail
            {
                PropertyId = PropertyId,
                Value = Value,
                Title = Title,
            };

        public override PropertyAdditionalDetail Update() =>
            new PropertyAdditionalDetail
            {
                PropertyId = PropertyId,
                Value = Value,
                Title = Title,
                Id = Id
            };
    }
}

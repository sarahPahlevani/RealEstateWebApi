using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class PriceScaleUnitDto : ModelDtoBase<PriceScaleUnit>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Scale { get; set; }

        public override IModelDto<PriceScaleUnit> From(PriceScaleUnit entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Scale = entity.Scale;
            return this;
        }

        public override PriceScaleUnit Create() =>
            new PriceScaleUnit
            {
                Scale = Scale,
                Name = Name,
            };

        public override PriceScaleUnit Update() =>
            new PriceScaleUnit
            {
                Id = Id,
                Name = Name,
                Scale = Scale,
            };
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class RegionDto : ModelDtoBase<Region>
    {
        public override int Id { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<Region> From(Region entity)
        {
            Id = entity.Id;
            CountryId = entity.CountryId;
            Name = entity.Name;
            return this;
        }

        public override Region Create() =>
            new Region
            {
                Name = Name,
                CountryId = CountryId,
            };

        public override Region Update() =>
            new Region
            {
                Id = Id,
                Name = Name,
                CountryId = CountryId,
            };
    }
}

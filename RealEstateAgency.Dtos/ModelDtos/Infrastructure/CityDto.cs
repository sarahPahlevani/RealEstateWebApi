using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class CityDto : ModelDtoBase<City>
    {
        public override int Id { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<City> From(City entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            RegionId = entity.RegionId;
            return this;
        }

        public override City Create() =>
            new City
            {
                Name = Name,
                RegionId = RegionId,
            };

        public override City Update() =>
            new City
            {
                Id = Id,
                Name = Name,
                RegionId = RegionId,
            };
    }
}

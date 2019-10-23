using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class ImportantPlaceTypeDto : ModelDtoBase<ImportantPlaceType>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] TypeIcon { get; set; }
        public string TypeIconImageUrl { get; set; }

        public override IModelDto<ImportantPlaceType> From(ImportantPlaceType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }

        public override ImportantPlaceType Create() =>
            new ImportantPlaceType
            {
                Name = Name,
                TypeIcon = TypeIcon
            };

        public override ImportantPlaceType Update() =>
            new ImportantPlaceType
            {
                Id = Id,
                Name = Name,
                TypeIcon = TypeIcon
            };
    }
}

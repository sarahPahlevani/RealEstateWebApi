using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyAroundImportantLandmarkDto : ModelDtoBase<PropertyAroundImportantLandmark>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int ImportantPlaceTypeId { get; set; }

        public string Description { get; set; }
        public decimal? DistanceMiles { get; set; }
        public decimal? DistanceKm { get; set; }
        public int? WalkToMin { get; set; }

        public override IModelDto<PropertyAroundImportantLandmark> From(PropertyAroundImportantLandmark entity)
        {
            Id = entity.Id;
            PropertyId = entity.PropertyId;
            ImportantPlaceTypeId = entity.ImportantPlaceTypeId;
            Description = entity.Description;
            DistanceMiles = entity.DistanceMiles;
            DistanceKm = entity.DistanceKm;
            WalkToMin = entity.WalkToMin;
            return this;
        }

        public override PropertyAroundImportantLandmark Create() =>
            new PropertyAroundImportantLandmark
            {
                PropertyId = PropertyId,
                Description = Description,
                DistanceKm = DistanceKm,
                DistanceMiles = DistanceMiles,
                ImportantPlaceTypeId = ImportantPlaceTypeId,
                WalkToMin = WalkToMin
            };

        public override PropertyAroundImportantLandmark Update() =>
            new PropertyAroundImportantLandmark
            {
                Id = Id,
                PropertyId = PropertyId,
                Description = Description,
                DistanceKm = DistanceKm,
                DistanceMiles = DistanceMiles,
                ImportantPlaceTypeId = ImportantPlaceTypeId,
                WalkToMin = WalkToMin
            };
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyDetailDto : ModelDtoBase<PropertyDetail>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        public decimal? Size { get; set; }
        public decimal? LandArea { get; set; }
        public int? Rooms { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Garages { get; set; }
        public decimal? GaragesSize { get; set; }
        public int? YearBuild { get; set; }

        public override IModelDto<PropertyDetail> From(PropertyDetail entity)
        {
            Id = entity.Id;
            PropertyId = entity.Id;
            Size = entity.Size;
            LandArea = entity.LandArea;
            Rooms = entity.Rooms;
            Bedrooms = entity.Bedrooms;
            Bathrooms = entity.Bathrooms;
            Garages = entity.Garages;
            GaragesSize = entity.GaragesSize;
            YearBuild = entity.YearBuild;
            return this;
        }

        public override PropertyDetail Create() =>
            new PropertyDetail
            {
                Id = PropertyId,
                Size = Size,
                LandArea = LandArea,
                Rooms = Rooms,
                Bedrooms = Bedrooms,
                Bathrooms = Bathrooms,
                Garages = Garages,
                GaragesSize = GaragesSize,
                YearBuild = YearBuild,
            };

        public override PropertyDetail Update() =>
            new PropertyDetail
            {
                Id = PropertyId,
                Size = Size,
                LandArea = LandArea,
                Rooms = Rooms,
                Bedrooms = Bedrooms,
                Bathrooms = Bathrooms,
                Garages = Garages,
                GaragesSize = GaragesSize,
                YearBuild = YearBuild,
            };
    }
}

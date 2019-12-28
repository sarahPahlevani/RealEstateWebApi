using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyLocationDto : ModelDtoBase<PropertyLocation>
    {
        public override int Id { get; set; }
        public int PropertyId { get; set; }
        //public int? CountryId { get; set; }
        //public int? RegionId { get; set; }
        //public int? CityId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        public string GoogleMapsLatitude { get; set; }
        public string GoogleMapsLongitude { get; set; }

        public override IModelDto<PropertyLocation> From(PropertyLocation entity)
        {
            Id = entity.Id;
            PropertyId = entity.Id;
            //CountryId = entity.CountryId;
            //RegionId = entity.RegionId;
            //CityId = entity.CityId;
            Country = entity.Country;
            Region = entity.Region;
            City = entity.City;
            ZipCode = entity.ZipCode;
            AddressLine1 = entity.AddressLine1;
            AddressLine2 = entity.AddressLine1;
            GoogleMapsLatitude = entity.GoogleMapsLatitude;
            GoogleMapsLongitude = entity.GoogleMapsLongitude;
            return this;
        }

        public override PropertyLocation Create() =>
            new PropertyLocation
            {
                Id = PropertyId,
                //CountryId = CountryId,
                //RegionId = RegionId,
                //CityId = CityId,
                Country = Country,
                Region = Region,
                City = City,
                ZipCode = ZipCode,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine1,
                GoogleMapsLatitude = GoogleMapsLatitude,
                GoogleMapsLongitude = GoogleMapsLongitude,
            };

        public override PropertyLocation Update() =>
        new PropertyLocation
        {
            Id = PropertyId,
            //CountryId = CountryId,
            //RegionId = RegionId,
            //CityId = CityId,
            Country = Country,
            Region = Region,
            City = City,
            ZipCode = ZipCode,
            AddressLine1 = AddressLine1,
            AddressLine2 = AddressLine1,
            GoogleMapsLatitude = GoogleMapsLatitude,
            GoogleMapsLongitude = GoogleMapsLongitude,
        };
    }
}

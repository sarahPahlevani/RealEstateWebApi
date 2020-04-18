using System.Collections.Generic;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos
{
    public class AdvancedSearchDto : PageRequestDto
    {
        public AdvancedSearchDto()
        {
            PageNumber = 0;
            PageSize = 10;
        }

        public List<int> PropertyTypeIds { get; set; }
        public List<int> PropertyStatusIds { get; set; }
        public List<int> PropertyLabelIds { get; set; }
        public List<int> PropertyFeatureIds { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string Search { get; set; }
        public int? SizeFrom { get; set; }
        public int? SizeTo { get; set; }
        public int? AreaFrom { get; set; }
        public int? AreaTo { get; set; }
        public int? RoomsFrom { get; set; }
        public int? RoomsTo { get; set; }
        public int? BedRoomsFrom { get; set; }
        public int? BedRoomsTo { get; set; }
        public int? BathRoomsFrom { get; set; }
        public int? BathRoomsTo { get; set; }
        public int? GaragesFrom { get; set; }
        public int? GaragesTo { get; set; }
        public int? GaragesSizeFrom { get; set; }
        public int? GaragesSizeTo { get; set; }
        public int? YearBuildFrom { get; set; }
        public int? YearBuildTo { get; set; }
        public int? FloorsFrom { get; set; }
        public int? FloorsTo { get; set; }
        public int? CityId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public int HasImage { get; set; }

    }
}
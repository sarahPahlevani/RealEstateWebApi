using RealEstateAgency.DAL.DtoContracts;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class PriceScaleUnitListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Scale { get; set; }
    }
}

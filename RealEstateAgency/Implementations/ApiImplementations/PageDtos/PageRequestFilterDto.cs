using Newtonsoft.Json.Linq;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos
{
    public class PageRequestFilterDto : PageRequestDto
    {
        public PageRequestFilterDto()
        {
        }

        public PageRequestFilterDto(int pageSize, int pageNumber) : base(pageSize, pageNumber)
        {
        }

        public JObject Filter { get; set; }
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.Contracts
{
    public interface IPaginationAction<TDto>
    where TDto : IDto
    {
        Task<PageResultDto<TDto>> GetPage(PageRequestDto pageArg, CancellationToken cancellationToken = default);
    }
}

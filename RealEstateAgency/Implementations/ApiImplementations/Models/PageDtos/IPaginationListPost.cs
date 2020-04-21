using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.DAL.DtoContracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos
{
    public interface IPaginationListPost<TListDto>
        where TListDto : IPaginationListDto
    {
        Task<ActionResult<PageResultDto<TListDto>>> GetList([FromBody] PageRequestFilterDto requestDto
            , CancellationToken cancellationToken);
    }
}

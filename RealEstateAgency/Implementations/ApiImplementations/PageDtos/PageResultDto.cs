using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos
{
    public interface IPagingResult<T> : IPageRequest
    {
        int Count { get; }
        IEnumerable<T> Items { set; get; }
        int Total { set; get; }

        Task<PageResultDto<T>> GetPage(CancellationToken cancellationToken);
    }

    public class PageResultDto<T> : IPagingResult<T>
    {
        private readonly IQueryable<T> _itemsQuery;
        public int Count => Items?.Count() ?? 0;
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public PageResultDto(IQueryable<T> itemsQuery, PageRequestDto req)
        {
            _itemsQuery = itemsQuery;
            PageNumber = req.PageNumber;
            PageSize = req.PageSize;
        }

        public PageResultDto(IEnumerable<T> items, PageRequestDto req, int total)
        {
            Items = items;
            PageNumber = req.PageNumber;
            PageSize = req.PageSize;
            Total = total;
        }

        public async Task<PageResultDto<T>> GetPage(CancellationToken cancellationToken = default)
        {
            if (_itemsQuery is null) return this;
            Total = await _itemsQuery.CountAsync(cancellationToken);
            Items = await _itemsQuery.Skip(PageSize * PageNumber).Take(PageSize)
                .ToListAsync(cancellationToken);
            return this;
        }
    }
}

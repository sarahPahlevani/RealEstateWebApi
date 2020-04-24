using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class ContentPageListFilter : BasePageFilter<ContentPage>, IAscendingPageFilter<ContentPage>,
        ISpecialSearchablePageFilter<ContentPage>
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => _searchText = string.IsNullOrWhiteSpace(value) ? null : value.ToLower().Trim();
        }

        public override IQueryable<ContentPage> Filter(IQueryable<ContentPage> entities)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(entities);
            var searchFilter = SearchFilter(entities);
            return AscendingFilter(searchFilter);
        }

        public IQueryable<ContentPage> SearchFilter(IQueryable<ContentPage> requests) =>
            string.IsNullOrWhiteSpace(SearchText)
                ? requests
                : requests
                    .Where(i => i.Title.ToLower().Contains(SearchText)||
                            i.ContentHeader.ToLower().Contains(SearchText) ||
                            i.ContectDetail.ToLower().Contains(SearchText) ||
                            i.ContentFooter.ToLower().Contains(SearchText) 
                    );

        public bool IsAscending { get; set; }

        public IQueryable<ContentPage> AscendingFilter(IQueryable<ContentPage> items) =>
            IsAscending
                ? items.OrderBy(i => i.Id)
                : items.OrderByDescending(i => i.Id);

        public IQueryable<ContentPage> SpecialSearchByFlags(IQueryable<ContentPage> items)
        {
            if (SearchText.Contains("id:"))
            {
                var textId = SearchText.Substring(3);
                if (string.IsNullOrWhiteSpace(textId)) return items;
                if (!int.TryParse(textId, out var id)) return items;
                return items.Where(i => i.Id == id);
            }

            return items;
        }

        public bool IsContainSpecialFlags() => !string.IsNullOrWhiteSpace(SearchText) &&
            (SearchText.StartsWith("id:"));
    }
}

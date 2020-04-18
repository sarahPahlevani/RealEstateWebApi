using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class AgentListFilter : BasePageFilter<Agent>, IAscendingPageFilter<Agent>,
        ISpecialSearchablePageFilter<Agent>
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => _searchText = string.IsNullOrWhiteSpace(value) ? null : value.ToLower().Trim();
        }

        public override IQueryable<Agent> Filter(IQueryable<Agent> entities)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(entities);
            var searchFilter = SearchFilter(entities);
            return AscendingFilter(searchFilter);
        }

        public IQueryable<Agent> SearchFilter(IQueryable<Agent> requests) =>
            string.IsNullOrWhiteSpace(SearchText)
                ? requests
                : requests.Include(i => i.UserAccount)
                    .Where(i => i.UserAccount.FirstName.ToLower().Contains(SearchText)
                                || i.UserAccount.LastName.ToLower().Contains(SearchText)
                                || i.UserAccount.UserName.ToLower().Contains(SearchText)
                                || i.UserAccount.Email.ToLower().Contains(SearchText)
                                || i.UserAccount.MiddleName.ToLower().Contains(SearchText));

        public bool IsAscending { get; set; }

        public IQueryable<Agent> AscendingFilter(IQueryable<Agent> items) =>
            IsAscending
                ? items.OrderBy(i => i.Id)
                : items.OrderByDescending(i => i.Id);

        public IQueryable<Agent> SpecialSearchByFlags(IQueryable<Agent> items)
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

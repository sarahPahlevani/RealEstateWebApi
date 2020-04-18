using System.Linq;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class UserAccountListFilter : BasePageFilter<UserAccount>, IAscendingPageFilter<UserAccount>,
        ISpecialSearchablePageFilter<UserAccount>
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => _searchText = string.IsNullOrWhiteSpace(value) ? null : value.ToLower().Trim();
        }

        public override IQueryable<UserAccount> Filter(IQueryable<UserAccount> entities)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(entities);
            var searchFilter = SearchFilter(entities);
            return AscendingFilter(searchFilter);
        }

        public IQueryable<UserAccount> SearchFilter(IQueryable<UserAccount> items)
        {
            return string.IsNullOrWhiteSpace(SearchText)
                ? items
                : items
                    .Where(i => i.FirstName.ToLower().Contains(SearchText)
                    || i.LastName.ToLower().Contains(SearchText)
                    || i.UserName.ToLower().Contains(SearchText)
                    || i.Email.ToLower().Contains(SearchText)
                    || i.MiddleName.ToLower().Contains(SearchText));
        }

        public bool IsAscending { get; set; }

        public IQueryable<UserAccount> AscendingFilter(IQueryable<UserAccount> requests)
        {
            return IsAscending
                ? requests.OrderBy(i => i.RegistrationDate)
                : requests.OrderByDescending(i => i.RegistrationDate);
        }

        public IQueryable<UserAccount> SpecialSearchByFlags(IQueryable<UserAccount> requests)
        {
            if (SearchText.Contains("id:"))
            {
                var textId = SearchText.Substring(3);
                if (string.IsNullOrWhiteSpace(textId)) return requests;
                if (!int.TryParse(textId, out var id)) return requests;
                return requests.Where(i => i.Id == id);
            }

            return requests;
        }

        public bool IsContainSpecialFlags() => !string.IsNullOrWhiteSpace(SearchText) &&
            (SearchText.StartsWith("id:"));
    }
}

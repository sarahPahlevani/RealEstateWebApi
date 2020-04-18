using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class UserGroupPermissionListFilter : BasePageFilter<UserGroupPermission>, IAscendingPageFilter<UserGroupPermission>,
        ISpecialSearchablePageFilter<UserGroupPermission>
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => _searchText = string.IsNullOrWhiteSpace(value) ? null : value.ToLower().Trim();
        }

        public override IQueryable<UserGroupPermission> Filter(IQueryable<UserGroupPermission> entities)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(entities);
            var searchFilter = SearchFilter(entities);
            return AscendingFilter(searchFilter);
        }

        public IQueryable<UserGroupPermission> SearchFilter(IQueryable<UserGroupPermission> requests) =>
            string.IsNullOrWhiteSpace(SearchText)
                ? requests
                : requests.Include(i => i.Menu)
                    .Where(i => i.Menu.Name.ToLower().Contains(SearchText))
            .Include(i => i.UserGroup).Where(i => i.UserGroup.Name.ToLower().Contains(SearchText));

        public bool IsAscending { get; set; }

        public IQueryable<UserGroupPermission> AscendingFilter(IQueryable<UserGroupPermission> items) =>
            IsAscending
                ? items.OrderBy(i => i.Id)
                : items.OrderByDescending(i => i.Id);

        public IQueryable<UserGroupPermission> SpecialSearchByFlags(IQueryable<UserGroupPermission> items)
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

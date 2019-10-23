using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;
using RealEstateAgency.Shared.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class RequestListFilter : BasePageFilter<Request>, IAscendingPageFilter<Request>,
        ISpecialSearchablePageFilter<Request>
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => _searchText = string.IsNullOrWhiteSpace(value) ? null : value.ToLower().Trim();
        }

        public override IQueryable<Request> Filter(IQueryable<Request> entities)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(entities);
            var searchFilter = SearchFilter(entities);
            return AscendingFilter(searchFilter);
        }

        public IQueryable<Request> SearchFilter(IQueryable<Request> requests)
        {
            return string.IsNullOrWhiteSpace(SearchText)
                ? requests
                : requests.Include(i => i.UserAccountIdRequesterNavigation)
                    .Include(i => i.RequestType)
                    .Where(i => i.Title.ToLower().Contains(SearchText)
                                        || i.UserAccountIdRequesterNavigation.Email.ToLower().Contains(SearchText)
                                        || i.UserAccountIdRequesterNavigation.FirstName.ToLower().Contains(SearchText)
                                        || i.UserAccountIdRequesterNavigation.LastName.ToLower().Contains(SearchText)
                                        || i.RequestType.Name.ToLower().Contains(SearchText));
        }

        public bool IsAscending { get; set; }

        public IQueryable<Request> AscendingFilter(IQueryable<Request> requests)
        {
            return IsAscending
                ? requests.OrderBy(i => i.DateCreated)
                : requests.OrderByDescending(i => i.DateCreated);
        }

        public IQueryable<Request> SpecialSearchByFlags(IQueryable<Request> requests)
        {
            if (SearchText.Contains("id:"))
            {
                var textId = SearchText.Substring(3);
                if (string.IsNullOrWhiteSpace(textId)) return requests;
                if (!int.TryParse(textId, out var id)) return requests;
                return requests.Where(i => i.Id == id);
            }
            if (SearchText.Contains("tn:"))
            {
                var textTn = SearchText.Substring(3);
                if (string.IsNullOrWhiteSpace(textTn)) return requests;
                return requests.Where(i => i.TrackingNumber == textTn);
            }

            if (SearchText.Contains("is:"))
            {
                var searchType = SearchText.Substring(3);
                switch (searchType)
                {
                    case "assigned":
                        return requests.Where(i => i.RequestAgent.Any(r => r.IsActive == true));
                    case "unassigned":
                        return requests.Where(i => !i.RequestAgent.Any());
                    case "apply":
                        return requests.Where(i => !i.RequestType.CanAddProperty);
                    case "submit":
                        return requests.Where(i => i.RequestType.CanAddProperty);
                    case "sell":
                        return requests.Where(i => i.RequestType.Name.Contains("sell"));
                    case "rent":
                        return requests.Where(i => i.RequestType.Name.Contains("rent"));
                    default: return requests;
                }
            }

            if (SearchText.Contains("request_type:"))
            {
                var searchType = SearchText.Substring("request_type:".Length);
                if (int.TryParse(searchType, out var requestTypeId))
                    return requests.Where(a => a.RequestTypeId == requestTypeId);

                throw new AppException("request id is not valid please try again;");
            }

            return requests;
        }

        public bool IsContainSpecialFlags() => !string.IsNullOrWhiteSpace(SearchText) &&
            (SearchText.StartsWith("id:") || SearchText.StartsWith("tn:")
                                          || SearchText.StartsWith("is:")
                                          || SearchText.StartsWith("request_type:"));
    }
}

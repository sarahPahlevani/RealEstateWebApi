using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class PropertyListFilter : BasePageFilter<Property>, IAscendingPageFilter<Property>
        , ISpecialSearchablePageFilter<Property>
    {
        public bool IsAscending { get; set; }
        public string SearchText { get; set; }

        public override IQueryable<Property> Filter(IQueryable<Property> entities)
            => AscendingFilter(SearchFilter(entities));

        public IQueryable<Property> SearchFilter(IQueryable<Property> properties)
        {
            if (IsContainSpecialFlags()) return SpecialSearchByFlags(properties);
            if (!string.IsNullOrWhiteSpace(SearchText))
                SearchText = SearchText.ToLower();

            return string.IsNullOrWhiteSpace(SearchText)
                ? properties
                : properties.Include(i => i.PropertyType)
                    .Include(i => i.PropertyLabel)
                    .Include(i => i.PropertyStatus)
                    .Where(i => i.Title.ToLower().Contains(SearchText)
                || i.PropertyLabel.Name.ToLower().Contains(SearchText)
                || i.PropertyType.Name.ToLower().Contains(SearchText)
                || i.PropertyStatus.Name.ToLower().Contains(SearchText));
        }

        public IQueryable<Property> AscendingFilter(IQueryable<Property> properties)
        {
            return IsAscending
                ? properties.OrderBy(i => i.PublishingDate)
                : properties.OrderByDescending(i => i.PublishingDate);
        }

        public IQueryable<Property> SpecialSearchByFlags(IQueryable<Property> items)
        {
            if (SearchText.Contains("id:"))
            {
                var textId = SearchText.Substring(3);
                if (string.IsNullOrWhiteSpace(textId)) return items;
                if (!int.TryParse(textId, out var id)) return items;
                return items.Where(i => i.Id == id);
            }

            if (SearchText.Contains("property_type:"))
            {
                var textId = SearchText.Replace("property_type:", "");
                if (string.IsNullOrWhiteSpace(textId)) return items;
                if (!int.TryParse(textId, out var id)) return items;
                return items.Where(i => i.PropertyTypeId == id);
            }

            if (SearchText.Contains("property_status:"))
            {
                var textId = SearchText.Replace("property_status:", "");
                if (string.IsNullOrWhiteSpace(textId)) return items;
                if (!int.TryParse(textId, out var id)) return items;
                return items.Where(i => i.PropertyStatusId == id);
            }

            if (SearchText.Contains("property_label:"))
            {
                var textId = SearchText.Replace("property_label:", "");
                if (string.IsNullOrWhiteSpace(textId)) return items;
                if (!int.TryParse(textId, out var id)) return items;
                return items.Where(i => i.PropertyLabelId == id);
            }

            if (SearchText.Contains("is:"))
            {
                var searchType = SearchText.Substring(3);
                switch (searchType)
                {
                    case "published":
                        return items.Where(i => i.IsPublished);
                    case "not_published":
                        return items.Where(i => !i.IsPublished);
                    case "publish_ready":
                        return items.Where(i => i.ReadyForPublish);
                    case "not_ready":
                        return items.Where(i => !i.ReadyForPublish);
                    default: return items;
                }
            }

            return items;
        }

        public bool IsContainSpecialFlags() => !string.IsNullOrWhiteSpace(SearchText) &&
                                               (SearchText.StartsWith("id:") ||
                                                SearchText.StartsWith("property_type:") ||
                                                SearchText.StartsWith("property_status:") ||
                                                SearchText.StartsWith("property_label:") ||
                                                SearchText.StartsWith("is:"));
    }
}

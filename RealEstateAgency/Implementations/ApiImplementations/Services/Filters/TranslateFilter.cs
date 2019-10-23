using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.Extensions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    internal class TranslateFilter<TEntity> : GetFilter<IQueryable<TEntity>>
        where TEntity : class, IEntity
    {
        private readonly int? _language;

        public TranslateFilter(int? language) => _language = language;

        public override IQueryable<TEntity> Filter(IQueryable<TEntity> filter)
            => _language is null ? filter : filter.Translate(_language.Value);
    }
}

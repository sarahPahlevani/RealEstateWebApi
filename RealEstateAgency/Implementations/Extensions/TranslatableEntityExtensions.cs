using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Contracts;
using System;
using System.Linq;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class TranslatableEntityExtensions
    {
        public static string GetTranslateObject(Type translateEntityType)
        {
            var prop = translateEntityType.GetProperties().FirstOrDefault(p => p.Name.Contains("Translate"));
            return prop != null ? prop.Name
                : throw new InvalidOperationException
                    ("The given type does not contains any translate object");
        }

        public static IQueryable<TType> Translate<TType>(this IQueryable<TType> query, int languageId)
            where TType : class, IEntity =>
            query.Include(GetTranslateObject(typeof(TType)))
                .Select(i => (i as ITranslatable<TType>).Translate(languageId));
    }
}

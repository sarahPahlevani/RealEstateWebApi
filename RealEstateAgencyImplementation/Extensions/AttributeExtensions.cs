using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealEstateAgency.Shared.AccessAttributes;

namespace RealEstateAgency.Shared.Extensions
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() is TAttribute att)
                return valueSelector(att);
            return default(TValue);
        }

        public static bool HasAttribute<TAttribute>(
            this Type type)
            where TAttribute : Attribute =>
            type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() != null;

        public static List<string> GetAccessAttributeRoles<TAttribute>(
            this Type type)
            where TAttribute : IUserAccessAttribute
        {
            if (type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() is TAttribute att)
                return att.AcceptRoles;
            return null;
        }
    }
}

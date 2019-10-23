using System;
using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns
{
    public abstract class FactoryBase
    {
        protected bool IsAssignableFrom<T>(Type interfaceType)
            where T : class => interfaceType.IsAssignableFrom(typeof(T));

        protected bool IsVirtualDelete<T>()
            where T : class => IsAssignableFrom<T>(typeof(IVirtualDelete));

        protected bool IsForbiddenDelete<T>()
            where T : class => IsAssignableFrom<T>(typeof(IForbiddenDelete));

        protected bool IsForbiddenCreate<T>()
            where T : class => IsAssignableFrom<T>(typeof(IForbiddenCreate));

        protected bool IsForbiddenUpdate<T>()
            where T : class => IsAssignableFrom<T>(typeof(IForbiddenUpdate));

        protected bool IsTranslatable<T>()
            where T : class
        {
            var genericInterface = typeof(T).GetInterfaces().FirstOrDefault(i => i.IsGenericType);
            return genericInterface != null &&
                   genericInterface.GetGenericTypeDefinition() == typeof(ITranslatable<>);
        }
    }
}

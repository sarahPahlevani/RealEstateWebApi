using System;
using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public class BaseGetService<T>
    where T : class, IEntity
    {
        protected bool IsTranslatable()
        {
            var genericInterface = typeof(T).GetInterfaces().FirstOrDefault(i => i.IsGenericType);
            return genericInterface != null &&
                   genericInterface.GetGenericTypeDefinition() == typeof(ITranslatable<>);
        }

        protected bool IsVirtualDelete()
            => IsAssignableFrom(typeof(IVirtualDelete));

        protected bool IsResultCache()
            => IsAssignableFrom(typeof(ICacheResult));

        protected bool IsAssignableFrom(Type interfaceType)
            => interfaceType.IsAssignableFrom(typeof(T));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.Shared.AccessAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class UserAccessAttribute : Attribute , IUserAccessAttribute
    {
        public List<string> AcceptRoles { get; protected set; }

        protected UserAccessAttribute(params string[] roles) => AcceptRoles = roles.ToList();
    }

    public interface IUserAccessAttribute 
    {
        List<string> AcceptRoles { get;}
    }
}
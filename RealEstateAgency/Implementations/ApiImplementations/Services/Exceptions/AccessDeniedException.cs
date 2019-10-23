using System;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions
{
     public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : base("Access to this action is forbidden for you")
        {

        }

        public AccessDeniedException(string action) : base($"Access to this {action} is forbidden for you")
        {

        }
    }
}

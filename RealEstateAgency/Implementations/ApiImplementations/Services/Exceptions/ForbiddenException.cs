using System;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "This action is forbidden") : base(message)
        {
        }
    }
}

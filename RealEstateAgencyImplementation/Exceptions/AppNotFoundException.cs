using System;

namespace RealEstateAgency.Shared.Exceptions
{
    public class AppNotFoundException : Exception
    {
        public AppNotFoundException()
        {
        }

        public AppNotFoundException(string message) : base(message)
        {
        }

        public AppNotFoundException(Exception e) : base(e.Message)
        {
        }
    }
}

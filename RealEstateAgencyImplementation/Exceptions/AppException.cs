using System;

namespace RealEstateAgency.Shared.Exceptions
{
    public class AppException : Exception
    {
        public bool LogError { get; }
        public AppException()
        {
        }

        public AppException(string message, bool logError = false) : base(message)
        {
            LogError = logError;
        }

        public AppException(Exception e) : base(e.Message)
        {
        }
    }
}

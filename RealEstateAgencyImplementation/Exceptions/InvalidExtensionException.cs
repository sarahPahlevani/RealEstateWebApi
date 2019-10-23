using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Shared.Exceptions
{
    class InvalidExtensionException : Exception
    {
        public InvalidExtensionException(string extension)
            :base("the extension {extension} is invalid")
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Dtos.Other
{

    public  class UpdateSignalDto
    {
        public string Message { get; set; }
        public string EntityName { get; set; }
        public string EntityCode { get; set; }
        public DateTime On { get; set; }
    }
}

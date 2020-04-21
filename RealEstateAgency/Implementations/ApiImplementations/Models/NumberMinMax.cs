using System.Collections.Generic;

namespace RealEstateAgency.Implementations.ApiImplementations.Models
{
    public class NumberMinMax 
    {

        public int RoomMin { get; set; }
        public int RoomMax { get; set; }

        public int BedMin { get; set; }
        public int BedMax { get; set; }

        public int BathMin { get; set; }
        public int BathMax { get; set; }

        public decimal SizeMin { get; set; }
        public decimal SizeMax { get; set; }

    }
}
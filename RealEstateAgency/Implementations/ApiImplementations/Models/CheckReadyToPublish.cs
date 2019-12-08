using System.Collections.Generic;

namespace RealEstateAgency.Implementations.ApiImplementations.Models
{
    public class CheckReadyToPublish
    {

        public bool Title { get; set; }
        public bool Size { get; set; }
        public bool Price { get; set; }
        public bool Media { get; set; }

        public bool All { get { return Title && Size && Price && Media; } }

    }
}
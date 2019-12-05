using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyWebAppImageDto
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public string ImagePath { get; set; }

        public string TumbPath { get; set; }
    }
}

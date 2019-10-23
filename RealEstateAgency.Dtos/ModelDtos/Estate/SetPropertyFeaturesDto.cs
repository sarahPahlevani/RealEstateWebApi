using RealEstateAgency.DAL.DtoContracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class SetPropertyFeaturesDto : IDto
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public List<int> PropertyFeatureIds { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.Other.Auth
{
    public class RegisterAgentDto : RegisterUserDto
    {
        [Required]
        public string RealEstateName { set; get; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.Other.Auth
{
    public class RegisterAgentDto : RegisterUserDto
    {
        
        public string RealEstateName { set; get; }

        [Required]
        public int RealEstateId { set; get; }

        public bool HasPublishingAuthorization { set; get; }

        public bool IsResponsible { set; get; }
    }
}
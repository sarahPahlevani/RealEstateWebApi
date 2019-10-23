using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateAgency.Dtos.ModelDtos.Organization
{
    public class AgentAccountDto
    {
        public int AgentId { get; set; }

        [Required]
        public bool IsResponsible { get; set; }
        [Required]
        public bool HasPublishingAuthorization { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string ZipCode { get; set; }
        public bool UpdatePassword { get; set; }
    }
}

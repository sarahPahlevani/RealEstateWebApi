using System.ComponentModel.DataAnnotations;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Dtos.Other.Auth
{
    public class RegisterUserDto : IRecaptchaForm
    {
        [Required]
        public string Firstname { set; get; }
        [Required]
        public string Lastname { set; get; }
        [Required]
        public string Username { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
        [Required]
        public string RecaptchaToken { get; set; }
    }
}
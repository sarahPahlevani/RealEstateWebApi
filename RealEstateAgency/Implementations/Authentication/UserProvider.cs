using System;
using System.Security.Claims;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.Authentication
{
    public class UserProvider : IUserProvider
    {
        public void SetUser(ClaimsPrincipal user, int? languageId = null)
        {
            if (user is null) return;
            if (!user.Identity.IsAuthenticated) return;
            LanguageId = languageId;
            IsAuthenticated = user.Identity.IsAuthenticated;
            Id = int.Parse(user.Identity.Name);

            foreach (var userClaim in user.Claims)
            {
                switch (userClaim.Type)
                {
                    case ClaimTypes.Role:
                        Role = userClaim.Value;
                        continue;
                    case ClaimTypes.Email:
                        Email = userClaim.Value;
                        continue;
                    case CustomClaimTypes.FullName:
                        FullName = userClaim.Value;
                        continue;
                    case CustomClaimTypes.ZipCode:
                        ZipCode = userClaim.Value;
                        continue;
                    case CustomClaimTypes.Address:
                        Address = userClaim.Value;
                        continue;
                    case CustomClaimTypes.Phone:
                        Phone = userClaim.Value;
                        continue;
                    case CustomClaimTypes.GroupId:
                        GroupId = int.Parse(userClaim.Value);
                        continue;
                    case CustomClaimTypes.RegistrationDate:
                        RegistrationDate = DateTime.Parse(userClaim.Value);
                        continue;
                    case CustomClaimTypes.RealEstateId:
                        RealEstateId = int.Parse(userClaim.Value);
                        continue;
                    case CustomClaimTypes.AgentId:
                        AgentId = int.Parse(userClaim.Value);
                        continue;
                    case CustomClaimTypes.IsResponsible:
                        IsResponsible = bool.Parse(userClaim.Value);
                        continue;
                    case CustomClaimTypes.HasPublishingAuthorization:
                        HasPublishingAuthorization = bool.Parse(userClaim.Value);
                        continue;
                }
            }
        }

        public bool IsAuthenticated { get; private set; }
        public int Id { get; private set; }
        public string Role { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string ZipCode { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public int? LanguageId { get; private set; }
        public int? RealEstateId { get; private set; }
        public int? AgentId { get; private set; }
        public bool IsAgent => AgentId != null && AgentId.Value > 0;
        public bool? IsResponsible { get; private set; }
        public bool? HasPublishingAuthorization { get; private set; }

        public int GroupId { get; private set; }

        public DateTime RegistrationDate { get; private set; }
    }

    public interface IUserProvider
    {
        void SetUser(ClaimsPrincipal user, int? languageId = null);

        bool IsAuthenticated { get; }
        int Id { get; }
        string Role { get; }
        string FullName { get; }
        string Email { get; }
        string ZipCode { get; }
        string Address { get; }
        string Phone { get; }
        int GroupId { get; }
        int? LanguageId { get; }
        int? RealEstateId { get; }
        int? AgentId { get; }
        bool IsAgent { get; }
        bool? IsResponsible { get; }
        bool? HasPublishingAuthorization { get; }

        DateTime RegistrationDate { get; }
    }
}

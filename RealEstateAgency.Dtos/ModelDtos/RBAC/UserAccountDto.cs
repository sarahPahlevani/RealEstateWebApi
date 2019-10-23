using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using RealEstateAgency.Dtos.Other.UserGroup;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserAccountDto : ModelDtoBase<UserAccount>
    {
        public override int Id { get; set; }
        public int? AuthenticationProviderId { get; set; }
        public string AuthenticationProviderAccessToken { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool? IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasExternalAuthentication { get; set; }
        public string ActivationKey { get; set; }
        public string ResetPasswordKey { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string ZipCode { get; set; }
        public string UserPicture { get; set; }
        public string UserPictureTumblr { get; set; }

        public UserGroupMetadata UserGroup { get; set; }
        public int? LanguageId { get; set; }
        public string Token { get; set; }
        public int? AgentId { get; set; }
        public int? RealEstateId { get; set; }
        public bool? IsResponsible { get; set; }
        public bool IsAgent { get; set; }
        public string ActivationLink { get; set; }
        public bool HasPublishingAuthorization { get; set; }

        public override IModelDto<UserAccount> From(UserAccount entity)
        {
            Id = entity.Id;
            AuthenticationProviderId = entity.AuthenticationProviderId;
            UserName = entity.UserName;
            IsActive = entity.IsActive;
            IsConfirmed = entity.IsConfirmed;
            HasExternalAuthentication = entity.HasExternalAuthentication;
            ActivationKey = entity.ActivationKey;
            RegistrationDate = entity.RegistrationDate;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            MiddleName = entity.MiddleName;
            Email = entity.Email;
            Phone01 = entity.Phone01;
            Phone02 = entity.Phone01;
            Address01 = entity.Address01;
            Address02 = entity.Address01;
            ZipCode = entity.ZipCode;
            return this;
        }

        public override UserAccount Create() =>
            new UserAccount
            {
                AuthenticationProviderId = AuthenticationProviderId,
                AuthenticationProviderAccessToken = AuthenticationProviderAccessToken,
                UserName = UserName,
                IsActive = IsActive,
                IsConfirmed = IsConfirmed,
                HasExternalAuthentication = HasExternalAuthentication,
                ActivationKey = ActivationKey,
                ResetPasswordKey = ResetPasswordKey,
                RegistrationDate = RegistrationDate,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                Email = Email,
                Phone01 = Phone01,
                Phone02 = Phone01,
                Address01 = Address01,
                Address02 = Address01,
                ZipCode = ZipCode,
            };

        public override UserAccount Update() =>
            new UserAccount
            {
                Id = Id,
                AuthenticationProviderId = AuthenticationProviderId,
                AuthenticationProviderAccessToken = AuthenticationProviderAccessToken,
                UserName = UserName,
                IsActive = IsActive,
                IsConfirmed = IsConfirmed,
                HasExternalAuthentication = HasExternalAuthentication,
                ActivationKey = ActivationKey,
                ResetPasswordKey = ResetPasswordKey,
                RegistrationDate = RegistrationDate,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                Email = Email,
                Phone01 = Phone01,
                Phone02 = Phone01,
                Address01 = Address01,
                Address02 = Address01,
                ZipCode = ZipCode,
            };
    }
}

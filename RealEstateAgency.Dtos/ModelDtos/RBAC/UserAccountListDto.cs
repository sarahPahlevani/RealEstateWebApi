using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserAccountListDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasExternalAuthentication { get; set; }
        public string ActivationKey { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string ZipCode { get; set; }
        public string VatCode { get; set; }
        public string UserPictureTumblr { get; set; }
        public string UserGroupName { get; set; }



        public decimal TotalEarn { get; set; }

        public decimal TotalWithdrawal { get; set; }
    }
}

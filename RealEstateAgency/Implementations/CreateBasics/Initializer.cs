using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Shared.Services;
using RealEstateAgency.Shared.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgency.Implementations.CreateBasics
{
    public class Initializer
    {
        private readonly RealEstateDbContext _context;
        private readonly IPasswordService _passwordService;

        private class CompanyFake
        {
            public string Name { get; set; }

            public List<UserFake> Users { get; set; } = new List<UserFake>();
        }

        private class UserFake
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            public string UserStaticCode { get; set; }
        }

        private readonly List<CompanyFake> companies = new List<CompanyFake>
        {
            new CompanyFake
            {
                Name = "AdminCompany",
                Users = new List<UserFake>
                {
                    new UserFake
                    {
                        Email = "miladbonak@gmail.com",
                        Lastname = "bonakdar",
                        Name = "milad",
                        Username = "miladbonak",
                        Password = "P@ssw0rd!P@ssw0rd",
                        UserStaticCode = UserGroups.Administrator
                    }
                }

            },
            new CompanyFake
            {
                Name = "test1",
                Users = new List<UserFake>
                {
                    new UserFake
                    {
                        Email = "agent@gmail.com",
                        Lastname = "ahmadi",
                        Name = "marjan",
                        Username = "agent",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.Agent
                    },
                    new UserFake
                    {
                        Email = "RealEstateAdministrator@gmail.com",
                        Lastname = "rahimi",
                        Name = "hamed",
                        Username = "RealEstateAdministrator",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.RealEstateAdministrator
                    },
                    new UserFake
                    {
                        Email = "MarketAssistance@gmail.com",
                        Lastname = "mosavi",
                        Name = "hossein",
                        Username = "MarketAssistance",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.MarketAssistance
                    },
                    new UserFake
                    {
                        Email = "MarketAssistancePlus@gmail.com",
                        Lastname = "vakili",
                        Name = "parviz",
                        Username = "MarketAssistancePlus",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.MarketAssistancePlus
                    },
                    new UserFake
                    {
                        Email = "AppClient@gmail.com",
                        Lastname = "golestan",
                        Name = "ali",
                        Username = "AppClient",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.AppClient
                    },
                    new UserFake
                    {
                        Email = "PropertyOwner@gmail.com",
                        Lastname = "bonakdar",
                        Name = "milad",
                        Username = "PropertyOwner",
                        Password = "P@ssw0rd",
                        UserStaticCode = UserGroups.PropertyOwner
                    }
                }
            },
        };


        public Initializer(RealEstateDbContext context,
            IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task Initialize()
        {
            foreach (var company in companies)
            {
                var mainCompany = await _context.RealEstate.FirstOrDefaultAsync(i => i.Name == company.Name);
                if (!(mainCompany is null)) continue;
                mainCompany = await CreateCompanyAsync(company);
                foreach (var user in company.Users)
                    CreateUser(mainCompany.Id, user);
            }
        }

        private void CreateUser(int realEstateId, UserFake account)
        {
            var groupId = _context.UserGroup.First(i => i.StaticCode == account.UserStaticCode).Id;

            var user = _context.UserAccount.Add(new UserAccount
            {
                Email = account.Email,
                HasExternalAuthentication = false,
                FirstName = account.Name,
                LastName = account.Lastname,
                IsConfirmed = true,
                IsActive = true,
                PasswordHash = _passwordService.HashUserPassword(account.Email, account.Password),
                RegistrationDate = DateTime.Now,
                UserName = account.Username
            });

            _context.UserAccountGroup.Add(new UserAccountGroup
            {
                DateCreated = DateTime.Now,
                IsActive = 1,
                UserAccountId = user.Entity.Id,
                UserGroupId = groupId
            });

            if (CheckIfShouldBeAgent(account.UserStaticCode))
                _context.Agent.Add(new Agent
                {
                    UserAccountId = user.Entity.Id,
                    RealEstateId = realEstateId,
                    MetadataJson = "{}",
                    IsResponsible = CheckIfShouldBeResponsible(account.UserStaticCode)
                });
            _context.SaveChanges();
        }

        private async Task<RealEstate> CreateCompanyAsync(CompanyFake company)
        {
            var languageId = _context.Language.First(i => i.IsDefault).Id;
            var currencyId = _context.Currency.First(i => i.IsDefault).Id;
            await _context.RealEstate.AddAsync(new RealEstate
            {
                Name = company.Name,
                CurrencyId = currencyId,
                LanguageIdDefault = languageId
            });
            await _context.SaveChangesAsync();
            return await _context.RealEstate.FirstOrDefaultAsync(i => i.Name == company.Name);
        }

        private bool CheckIfShouldBeResponsible(string staticCode) =>
            UserGroups.Administrator == staticCode ||
            UserGroups.RealEstateAdministrator == staticCode ||
            UserGroups.Agent == staticCode;

        private bool CheckIfShouldBeAgent(string staticCode) =>
            UserGroups.RealEstateAdministrator == staticCode ||
            UserGroups.MarketAssistance == staticCode ||
            UserGroups.MarketAssistancePlus == staticCode ||
            UserGroups.Agent == staticCode;
    }

}

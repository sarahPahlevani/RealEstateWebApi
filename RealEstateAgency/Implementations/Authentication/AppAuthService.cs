using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using RealEstateAgency.Dtos.Other.Auth;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Services;
using RealEstateAgency.Shared.Statics;
using UserGroup = RealEstateAgency.Dtos.Other.UserGroup.UserGroup;

namespace RealEstateAgency.Implementations.Authentication
{
    public class AppAuthService : IAppAuthService
    {
        private readonly IEntityService<UserAccount> _entityService;
        private readonly IEntityService<RealEstate> _estateEntityService;
        private readonly IPasswordService _passwordService;
        private readonly IFastHasher _hasher;
        private readonly IUserGroupProvider _groupProvider;
        private readonly AuthSetting _authSetting;

        public AppAuthService(IEntityService<UserAccount> entityService, IEntityService<RealEstate> estateEntityService, IOptions<AuthSetting> authOptions,
            IPasswordService passwordService, IFastHasher hasher, IUserGroupProvider groupProvider)
        {
            _entityService = entityService;
            _estateEntityService = estateEntityService;
            _passwordService = passwordService;
            _hasher = hasher;
            _groupProvider = groupProvider;
            _authSetting = authOptions.Value;
        }

        public async Task<UserAccountDto> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            email = email.ToLower().Trim();
            var userData = await _entityService.Queryable
                .Select(i => new
                {
                    user = i,
                    role = i.UserAccountGroup.First().UserGroup.StaticCode,
                    groupId = i.UserAccountGroup.First().UserGroup.Id,
                })
                .FirstOrDefaultAsync(i => i.user.Email == email || i.user.UserName == email, cancellationToken);

            if (userData == null) throw new AppNotFoundException("User cannot be found");

            var user = userData.user;
            var role = userData.role;
            var groupId = userData.groupId;

            if (user.IsConfirmed == false) throw new AppException("Please confirm your email");
            if (user.IsActive == false) throw new AppException("User is deactivated");
            if (!_passwordService.VerifyUser(user.Email, password, user.PasswordHash))
                throw new AppException("Username or password is invalid");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(user, role, groupId),
                Expires = DateTime.UtcNow.AddDays(_authSetting.ExpireInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userDto = new UserAccountDto();
            userDto.From(user);
            userDto.Token = tokenHandler.WriteToken(token);
            return userDto;
        }

        public async Task<UserAccountDto> RegisterAgentAsync(RegisterAgentDto registerAgentDto, CancellationToken cancellationToken)
        {
            registerAgentDto.Email = registerAgentDto.Email.ToLower().Trim();
            registerAgentDto.Username = registerAgentDto.Username.ToLower().Trim();

            var estate = await CheckIfEstateExistById(registerAgentDto.RealEstateId, cancellationToken);
            await CheckIfUserIsValid(registerAgentDto.Email, registerAgentDto.Username, cancellationToken);
            var activationKey = _hasher.CalculateHash(registerAgentDto.Firstname
                                                      + registerAgentDto.Lastname
                                                      + registerAgentDto.Email
                                                      + registerAgentDto.Username);
            var passwordHash = _passwordService.HashUserPassword(registerAgentDto.Email, registerAgentDto.Password);
            var user = CreateUser(registerAgentDto, UserGroup.Agent, passwordHash, activationKey);

            _entityService.DbContext.Agent.Add(new Agent
            {
                RealEstateId = estate.Id,
                UserAccountId = user.Id,
                MetadataJson = "{}"
            });
            _entityService.DbContext.SaveChanges();
            return await AuthenticateAsync(registerAgentDto.Email, registerAgentDto.Password, cancellationToken);
        }

        public async Task<UserAccountDto> RegisterAsync(RegisterUserDto registerDto, CancellationToken cancellationToken)
        {
            registerDto.Email = registerDto.Email.ToLower().Trim();
            registerDto.Username = registerDto.Username.ToLower().Trim();

            var estate = await _estateEntityService.Queryable.FirstAsync(cancellationToken);

            await CheckIfUserIsValid(registerDto.Email, registerDto.Username, cancellationToken);

            var activationKey = _hasher.CalculateHash(registerDto.Firstname
                                                      + registerDto.Lastname
                                                      + registerDto.Email
                                                      + registerDto.Username);
            var passwordHash = _passwordService.HashUserPassword(registerDto.Email, registerDto.Password);
            CreateUser(registerDto, UserGroup.AppClient, passwordHash, activationKey);

            _entityService.DbContext.SaveChanges();
            return await AuthenticateAsync(registerDto.Email, registerDto.Password, cancellationToken);

        }

        private UserAccount CreateUser(RegisterUserDto registerAgentDto, UserGroup userGroup, string passwordHash = null, string activationKey = null)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserAccount user;
                try
                {
                    user = _entityService.Create(new UserAccount
                    {
                        ActivationKey = activationKey,
                        Email = registerAgentDto.Email,
                        UserName = registerAgentDto.Username,
                        FirstName = registerAgentDto.Firstname,
                        LastName = registerAgentDto.Lastname,
                        HasExternalAuthentication = false,
                        IsConfirmed = false,
                        PasswordHash = passwordHash,
                        IsActive = true,
                        RegistrationDate = DateTime.Now,
                        ReferralCode = _hasher.CalculateHash(nameof(UserAccount.ReferralCode), activationKey)
                    });

                    _entityService.DbContext.UserAccountGroup.Add(new UserAccountGroup
                    {
                        DateCreated = DateTime.Now,
                        IsActive = 1,
                        UserAccountId = user.Id,
                        UserGroupId = _groupProvider[userGroup].Id,
                    });
                    _entityService.DbContext.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
                return user;
            }
        }

        public async Task<UserAccount> UpdatePasswordAsync(int userId, string newPassword)
        {
            return await UpdatePassword(userId, newPassword);
        }

        private async Task<UserAccount> UpdatePassword(int userId, string newPassword)
        {
            var userAccount = _entityService.DbContext.UserAccount.Where(t => t.Id == userId).FirstOrDefault();
            userAccount.PasswordHash = _passwordService.HashUserPassword(userAccount.Email, newPassword);
            await _entityService.UpdateAsync(userAccount);
            await _entityService.DbContext.SaveChangesAsync();
            return userAccount;
        }

        public async Task CheckIfUserIsValid(string email, string username, CancellationToken cancellationToken)
        {
            email = email.ToLower().Trim();
            username = username.ToLower().Trim();
            var dupUser = await _entityService.GetAsync(user => (user.Email != null && user.Email == email)
                                                                || (user.UserName != null && user.UserName == username),
                cancellationToken);
            if (dupUser != null)
            {
                if (dupUser.Email == email) throw new AppException("User with this email exist");
                if (dupUser.UserName == username) throw new AppException("User with this username exist");
            }
        }

        private async Task<RealEstate> CheckIfEstateExist(string estateName, CancellationToken cancellationToken)
        {
            var estate = await _estateEntityService.GetAsync(e => e.Name == estateName, cancellationToken);
            if (estate is null) throw new AppException("Estate is not valid");
            return estate;
        }

        private async Task<RealEstate> CheckIfEstateExistById(int id, CancellationToken cancellationToken)
        {
            var estate = await _estateEntityService.GetAsync(e => e.Id == id, cancellationToken);
            if (estate is null) throw new AppException("Estate is not valid");
            return estate;
        }

        private ClaimsIdentity GetClaimsIdentity(UserAccount account, string role, int groupId)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, account.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(CustomClaimTypes.FullName, $"{account.FirstName} {account.MiddleName} {account.LastName}"),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(CustomClaimTypes.ZipCode, string.IsNullOrEmpty(account.ZipCode) ? "" : account.ZipCode),
                new Claim(CustomClaimTypes.Address,
                    string.IsNullOrEmpty(account.Address01) ? "" : $"{account.Address01},{account.Address02}"),
                new Claim(CustomClaimTypes.Phone,
                    string.IsNullOrEmpty(account.Phone01) ? "" : $"{account.Phone01},{account.Phone02}"),
                new Claim(CustomClaimTypes.GroupId, groupId.ToString()),
                new Claim(CustomClaimTypes.RegistrationDate,
                    account.RegistrationDate.ToString(CultureInfo.InvariantCulture))
            });
            if (role == UserGroups.Agent
                //|| role == UserGroups.Administrator
                || role == UserGroups.RealEstateAdministrator)
            {
                var agent = GetAgent(account.Id);

                claims.AddClaim(new Claim(CustomClaimTypes.AgentId,
                    agent.Id.ToString()));

                claims.AddClaim(new Claim(CustomClaimTypes.IsResponsible,
                    agent.IsResponsible.ToString()));

                claims.AddClaim(new Claim(CustomClaimTypes.HasPublishingAuthorization,
                    agent.HasPublishingAuthorization.ToString()));

                claims.AddClaim(new Claim(CustomClaimTypes.RealEstateId,
                    agent.RealEstateId.ToString()));
            }

            return claims;
        }

        private Agent GetAgent(int userId)
        {
            var agent = _entityService.DbContext.Agent.Include(a => a.RealEstate)
                .FirstOrDefault(a => a.UserAccountId == userId);
            if (agent is null) throw new AppException("User is not Agent");
            return agent;
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using RealEstateAgency.Dtos.Other;
using RealEstateAgency.Dtos.Other.Auth;
using RealEstateAgency.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealEstateAgency.Dtos.ModelDtos.Organization;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Implementations.Services;
using RealEstateAgency.Mailables;
using RealEstateAgency.Shared.BaseModels;
using static RealEstateAgency.Controllers.Other.PublicController;
using System;
using RealEstateAgency.Shared.Statics;
using RealEstateAgency.Shared.Services;

namespace RealEstateAgency.Controllers.Authentication
{
    public class AuthController : BaseApiController
    {
        private readonly IAppAuthService _appAuthService;
        private readonly IModelService<UserAccount, UserAccountDto> _userAccount;
        private readonly IEntityService<RealEstate> _estateService;
        private readonly IRecaptchaService _recaptchaService;
        private readonly IUserProvider _userProvider;
        private readonly IMailer _mailer;
        private readonly IUserGroupProvider _groupProvider;
        private readonly AppSetting _appSetting; 
        private readonly IEntityService<UserAccount> _userService;
        private readonly IPasswordService _passwordService;

        public AuthController(IAppAuthService appAuthService, IEntityService<UserAccount> userService
            , IModelService<UserAccount, UserAccountDto> userAccount
            , IUserProvider userProvider,IMailer mailer,IOptions<AppSetting> appSetting
            , IUserGroupProvider groupProvider, IEntityService<RealEstate> estateService
            , IRecaptchaService recaptchaService
            , IPasswordService passwordService)
        {
            _appAuthService = appAuthService;
            _userAccount = userAccount;
            _userProvider = userProvider;
            _mailer = mailer;
            _groupProvider = groupProvider;
            _estateService = estateService;
            _recaptchaService = recaptchaService;
            _appSetting = appSetting.Value;
            _userService = userService;
            _passwordService = passwordService;
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public string test()
        {
            return "test";
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<ActionResult<UserAccountDto>> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var userDto = await _appAuthService.AuthenticateAsync
                (loginDto.UsernameOrEmail, loginDto.Password, cancellationToken);
            return userDto;
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<ActionResult<UserAccountDto>> RegisterAgent([FromBody] RegisterAgentDto registerAgentDto, CancellationToken cancellationToken)
        {
            var userDto = await _appAuthService.RegisterAgentAsync
                (registerAgentDto, cancellationToken);
            return userDto;
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<ActionResult<UserAccountDto>> Register([FromBody] RegisterUserDto registerDto, CancellationToken cancellationToken)
        {
            //if(!await _recaptchaService.Validate(registerDto.RecaptchaToken, cancellationToken))
            //    throw new AppException("The recaptcha was failed",true);
            var userDto = await _appAuthService.RegisterAsync(registerDto, cancellationToken);
            //TODO: this code commented due to error from google that says tooweekawutontication. less secure apps
            await _mailer.SendAsync(new UserEmailActivationEmail(userDto,
                $"{_appSetting.ApiBaseUrl}/api/public/VerifyEmail/", _appSetting.AdminEmailAddress));
            return userDto;
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<CheckUserResultDto> CheckUser([FromBody] UserCheckDto registerAgentDto, CancellationToken cancellationToken)
        {
            try
            {
                await _appAuthService.CheckIfUserIsValid(registerAgentDto.Email, registerAgentDto.Username,
                    cancellationToken);
                return new CheckUserResultDto
                {
                    ErrorMessage = null,
                    Message = "user is valid",
                    Ok = true,
                };
            }
            catch (AppException appException)
            {
                var message = new CheckUserResultDto
                {
                    ErrorMessage = appException.Message,
                    Message = null,
                    Ok = false,
                    EmailHasProblem = appException.Message.Contains("email"),
                    UsernameHasProblem = appException.Message.Contains("username")
                };
                return message;
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserAccountDto>> GetCurrentUser(CancellationToken cancellationToken)
        {
            var user = await _userAccount.GetDtoAsync(_userProvider.Id, cancellationToken);
            user.UserGroup = _groupProvider[_userProvider.Role];
            user.LanguageId = _userProvider.LanguageId;
            user.AgentId = _userProvider.AgentId;
            user.IsResponsible = _userProvider.IsResponsible;
            user.HasPublishingAuthorization = _userProvider.HasPublishingAuthorization == true;
            user.RealEstateId = _userProvider.RealEstateId;
            user.IsAgent = _userProvider.IsAgent;
            return user;
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<RealEstateDto>> GetUserRealEstate(CancellationToken cancellationToken)
        {
            if (_userProvider.RealEstateId is null) return null;
            var estate = await _estateService.Queryable
                .FirstAsync(i => i.Id == _userProvider.RealEstateId.Value, cancellationToken);
            return new RealEstateDto
            {
                Id = estate.Id,
                Name = estate.Name,
                Email = estate.Email,
                CurrencyId = estate.CurrencyId,
                Address01 = estate.Address01,
                Phone01 = estate.Phone01,
                DateFormat = estate.DateFormat,
                ZipCode = estate.ZipCode,
                LanguageIdDefault = estate.LanguageIdDefault,
                Phone02 = estate.Phone01,
                Address02 = estate.Address01,
                Fax = estate.Fax,
                Phone03 = estate.Phone01,
                WebsiteUrl = estate.WebsiteUrl
            };
        }


        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPost("[Action]")]
        public async Task<ActionResult> AdminChangePassword(int id, string password)
        {
            return await UserChangePassword(id, null, password);
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult> UserChangePassword(int id, string oldPassword, string newPassword)
        {
            if (_userProvider.Id != id && _userProvider.Role != UserGroups.Administrator && _userProvider.Role != UserGroups.RealEstateAdministrator)
                return Forbid();

            var user = await _userService.GetAsync(u => u.Id == id);
            if (user is null)
                throw new AppNotFoundException("User cannot be found");

            if (_userProvider.Role != UserGroups.Administrator && _userProvider.Role != UserGroups.RealEstateAdministrator)
                if (!_passwordService.VerifyUser(user.Email, oldPassword, user.PasswordHash))
                    throw new AppException("Current password is invalid");

            await _appAuthService.UpdatePasswordAsync(user.Id, newPassword);

            return Ok();
        }



        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<ActionResult<string>> RequestResetPassword(LoginDto login, CancellationToken cancellationToken)
        {
            var userAccountitem = _userAccount.Get(t => t.Email == login.UsernameOrEmail && t.IsActive == true);
            if (userAccountitem == null)
            {
                throw new AppException("User not found.", true);
            }
            string resetPasswordKey = Guid.NewGuid().ToString("d");
            userAccountitem.ResetPasswordKey = resetPasswordKey;
            _userAccount.Update(userAccountitem);

            await _mailer.SendAsync(new UserResetPasswordEmail(userAccountitem,
                $"{_appSetting.ApiBaseUrl}api/public/ResetPassword/", _appSetting.AdminEmailAddress));

            return resetPasswordKey;
        }


        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<ContentResult> ResetPassword([FromForm]string password, [FromForm]string resetKeyPassword)
        {

            var user = await _userService.GetAsync(u => u.ResetPasswordKey == resetKeyPassword);

            if (user is null) return await RenderResetPasswordResult(new ResetPasswordDto
            {
                Message = "This key does not exist",
            });

            user.ResetPasswordKey = "";
            await _userService.UpdateAsync(user);
            await _appAuthService.UpdatePasswordAsync(user.Id, password);


            return await RenderResetPasswordResult(new ResetPasswordDto
            {
                Message = "Your password changed successfully.",
                ReturnUrl = _appSetting.WebAppBaseUrl,
                Ok = true
            });
        }
        private async Task<ContentResult> RenderResetPasswordResult(ResetPasswordDto dto)
        {
            var res = await _mailer.RenderAsync(new UserResetPasswordPage(dto));
            return Content(res, "text/html");
        }
    }
}

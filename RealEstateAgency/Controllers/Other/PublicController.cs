using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Mailables;
using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Exceptions;

namespace RealEstateAgency.Controllers.Other
{
    [AllowAnonymous]
    public class PublicController : BaseApiController
    {
        private readonly IEntityService<UserAccount> _userService;
        private readonly IEntityService<RealEstate> _entityService;
        private readonly IMailer _mailer;
        private readonly AppSetting _appSetting;

        public PublicController(IOptions<AppSetting> appSettingOption,
            IEntityService<UserAccount> userService, IEntityService<RealEstate> entityService,
            IMailer mailer)
        {
            _userService = userService;
            _entityService = entityService;
            _mailer = mailer;
            _appSetting = appSettingOption.Value;
        }

        [HttpGet("[Action]")]
        public AppInformation SiteInfo() => _appSetting.Info;

        [HttpGet("[Action]")]
        public object RealEstateInfo() => _entityService.Queryable
            .Include(e => e.Currency).FirstOrDefault();

        public class VerifyEmailDto
        {
            public string Message { get; set; }
            public bool Ok { get; set; } = false;
            public string ReturnUrl { get; set; }
        }

        public class ResetPasswordDto
        {
            public string Message { get; set; }
            public bool Ok { get; set; } = false;
            public string ReturnUrl { get; set; }
            public string ResetKeyPassword { get; set; }
        }

        [HttpGet("[Action]/{activationCode}")]
        public async Task<ContentResult> VerifyEmail(string activationCode, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(activationCode) || activationCode.Length < 5)
                return await RenderVerifyEmailResult(new VerifyEmailDto
                {
                    Message = "This Activation key is not valid"
                });
            var user = await _userService.GetAsync(u => u.ActivationKey == activationCode, cancellationToken);

            if (user is null) return await RenderVerifyEmailResult(new VerifyEmailDto
            {
                Message = "This Activation key does not exist",
            });

            user.ActivationKey = "";
            user.IsConfirmed = true;
            await _userService.UpdateAsync(user, cancellationToken);

            return await RenderVerifyEmailResult(new VerifyEmailDto
            {
                Message = "Your email has been verified",
                ReturnUrl = _appSetting.WebAppBaseUrl,
                Ok = true
            });
        }

        [HttpGet("[Action]/{resetPasswordKey}")]
        public async Task<ContentResult> ResetPassword(string resetPasswordKey, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordKey) || resetPasswordKey.Length < 5)
                return await RenderResetPasswordResult(new ResetPasswordDto
                {
                    Message = "This key is not valid"
                });
            var user = await _userService.GetAsync(u => u.ResetPasswordKey == resetPasswordKey, cancellationToken);

            if (user is null) return await RenderResetPasswordResult(new ResetPasswordDto
            {
                Message = "This key does not exist",
            });

            //user.ResetPasswordKey = "";
            //await _userService.UpdateAsync(user, cancellationToken);

            return await RenderResetPasswordResult(new ResetPasswordDto
            {
                Message = "please type your new password.",
                ReturnUrl = _appSetting.WebAppBaseUrl,
                Ok = true,
                ResetKeyPassword = resetPasswordKey
            });
        }

        private async Task<ContentResult> RenderVerifyEmailResult(VerifyEmailDto dto)
        {
            var res = await _mailer.RenderAsync(new VerifyEmailPage(dto));
            return Content(res, "text/html");
        }

        private async Task<ContentResult> RenderResetPasswordResult(ResetPasswordDto dto)
        {
            var res = await _mailer.RenderAsync(new UserREsetPasswordPage(dto));
            return Content(res, "text/html");
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<Subscribes>> Subscribe(Subscribes dto)
        {
            var item = _entityService.DbContext.Subscribes.Where(t => t.Email == dto.Email.ToLower()).FirstOrDefault();
            if (item == null)
            {
                dto.Email = dto.Email.ToLower();
                dto.InsertDateTime = DateTime.Now;
                _entityService.DbContext.Subscribes.Add(dto);
                await _entityService.DbContext.SaveChangesAsync();
                return dto;
            }
            else
            {
                new AppException("This email has already been subscribed.");
                return item;
            }

        }
    }
}

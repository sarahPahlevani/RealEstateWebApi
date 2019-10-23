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

        private async Task<ContentResult> RenderVerifyEmailResult(VerifyEmailDto dto)
        {
            var res = await _mailer.RenderAsync(new VerifyEmailPage(dto));
            return Content(res, "text/html");
        }
    }
}

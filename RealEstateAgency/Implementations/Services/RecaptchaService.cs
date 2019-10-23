using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Dtos.Other.Recaptcha;
using RealEstateAgency.Shared.BaseModels;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Shared.Exceptions;

namespace RealEstateAgency.Implementations.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly RecaptchaSetting _recaptchaSetting;
        private readonly HttpClient _httpClient;

        public RecaptchaService(IOptions<RecaptchaSetting> options)
        {
            _recaptchaSetting = options.Value;
            _httpClient = new HttpClient();
        }

        public async Task<bool> Validate(string userToken, CancellationToken cancellationToken = default)
        {
            var res = await _httpClient.PostAsync(_recaptchaSetting.VerifyUrl, CreateRecaptchaReqBody(userToken), cancellationToken);
            var result = JsonConvert.DeserializeObject<RecaptchaResponseDto>(await res.Content.ReadAsStringAsync());
            if (result.Success) return true;
            var err = RecaptchaErrorItem.GetErrorsMessage(result.ErrorCodes);
            throw new AppException(err);
        }

        public Task<bool> Validate(IRecaptchaForm userToken, CancellationToken cancellationToken = default)
            => Validate(userToken.RecaptchaToken, cancellationToken);

        private HttpContent CreateRecaptchaReqBody(string token) =>
            new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", _recaptchaSetting.SiteToken),
                    new KeyValuePair<string, string>("response", token)
                });
    }

    public interface IRecaptchaService
    {
        Task<bool> Validate(string userToken, CancellationToken cancellationToken = default);

        Task<bool> Validate(IRecaptchaForm userToken, CancellationToken cancellationToken = default);
    }
}

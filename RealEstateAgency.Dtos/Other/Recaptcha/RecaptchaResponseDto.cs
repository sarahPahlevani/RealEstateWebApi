using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RealEstateAgency.Dtos.Other.Recaptcha
{
    public class RecaptchaResponseDto
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error-codes")]
        public IEnumerable<string> ErrorCodes { get; set; }
    }

    public class RecaptchaErrorItem
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public static Dictionary<string, RecaptchaErrorItem> RecaptchaErrors
            = new Dictionary<string, RecaptchaErrorItem>
            {
                {
                    "missing-input-secret", new RecaptchaErrorItem
                    {
                        Code = "missing-input-secret",
                        Description = "The secret parameter is missing."
                    }
                }, {
                    "invalid-input-secret", new RecaptchaErrorItem
                    {
                        Code = "invalid-input-secret",
                        Description = "The secret parameter is invalid or malformed."
                    }
                }, {
                    "missing-input-response", new RecaptchaErrorItem
                    {
                        Code = "missing-input-response",
                        Description = "The response parameter is missing."
                    }
                }, {
                    "invalid-input-response", new RecaptchaErrorItem
                    {
                        Code = "invalid-input-response",
                        Description = "The response parameter is invalid or malformed."
                    }
                }, {
                    "bad-request", new RecaptchaErrorItem
                    {
                        Code = "bad-request",
                        Description = "The request is invalid or malformed."
                    }
                }, {
                    "timeout-or-duplicate", new RecaptchaErrorItem
                    {
                        Code = "timeout-or-duplicate",
                        Description = "The response is no longer valid: either is too old or has been used previously."
                    }
                }
            };

        public static string GetErrorsMessage(IEnumerable<string> errors)
        {
            if (errors is null || !errors.Any())
                return "Something goes wrong when trying to check the Recaptcha token";
            var err = errors.Aggregate("", (a, b) =>
                a + " _ " + RecaptchaErrorItem.RecaptchaErrors[b].Description);
            if (err.StartsWith(" _ ")) err = err.Substring(3);
            return err;
        }
    }

}

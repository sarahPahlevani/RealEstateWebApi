using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.DAL.Contracts
{
    public interface IRecaptchaForm
    {
        string RecaptchaToken { get; set; }
    }
}

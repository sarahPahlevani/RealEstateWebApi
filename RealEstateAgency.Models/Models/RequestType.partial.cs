using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class RequestType : IEntity, ITranslatable<RequestType>, ICacheResult, IForbiddenModify
    {
        public RequestType Translate(int languageId)
        {
            return new RequestType
            {
                Id = Id,
                Name = RequestTypeTranslate.Any(t => t.RequestTypeId == Id && t.LanguageId == languageId)
                    ? RequestTypeTranslate.First().Name
                    : Name,
                CanAddProperty = CanAddProperty,
            };
        }
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestTypeTranslateDto : ModelDtoBase<RequestTypeTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestTypeId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<RequestTypeTranslate> From(RequestTypeTranslate entity)
        {
            Id = entity.Id;
            RequestTypeId = entity.RequestTypeId;
            Name = entity.Name;
            LanguageId = entity.LanguageId;
            return this;
        }

        public override RequestTypeTranslate Create() =>
            new RequestTypeTranslate
            {
                Name = Name,
                LanguageId = LanguageId,
                RequestTypeId = RequestTypeId
            };

        public override RequestTypeTranslate Update() =>
            new RequestTypeTranslate
            {
                Id = Id,
                Name = Name,
                LanguageId = LanguageId,
                RequestTypeId = RequestTypeId
            };
    }
}

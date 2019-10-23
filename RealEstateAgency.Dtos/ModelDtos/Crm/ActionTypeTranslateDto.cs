using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class ActionTypeTranslateDto : ModelDtoBase<ActionTypeTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int ActionTypeId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<ActionTypeTranslate> From(ActionTypeTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LanguageId = entity.LanguageId;
            ActionTypeId = entity.ActionTypeId;
            return this;
        }

        public override ActionTypeTranslate Create() =>
            new ActionTypeTranslate
            {
                ActionTypeId = ActionTypeId,
                LanguageId = LanguageId,
                Name = Name,
            };

        public override ActionTypeTranslate Update() =>
            new ActionTypeTranslate
            {
                ActionTypeId = ActionTypeId,
                LanguageId = LanguageId,
                Name = Name,
                Id = Id
            };
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class ActionTypeDto : ModelDtoBase<ActionType>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<ActionType> From(ActionType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }

        public override ActionType Create() =>
            new ActionType
            {
                Name = Name
            };

        public override ActionType Update() =>
            new ActionType
            {
                Name = Name,
                Id = Id
            };
    }
}

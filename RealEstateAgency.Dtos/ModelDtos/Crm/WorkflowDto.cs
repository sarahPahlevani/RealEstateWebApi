using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class WorkflowDto : ModelDtoBase<Workflow>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int RequestTypeId { get; set; }

        public override IModelDto<Workflow> From(Workflow entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            RequestTypeId = entity.RequestTypeId;
            return this;
        }

        public override Workflow Create() =>
            new Workflow
            {
                Name = Name,
                RequestTypeId = RequestTypeId
            };

        public override Workflow Update() =>
            new Workflow
            {
                Id = Id,
                Name = Name,
                RequestTypeId = RequestTypeId
            };
    }
}

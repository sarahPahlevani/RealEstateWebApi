using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class MenuDto : ModelDtoBase<Menu>
    {
       // public  int Id { get; set; }
        public override int Id { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string Name { get; set; }
        public bool IsPanelPage { get; set; }
        public int? ParentId { get; set; }
        public string PluginName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public override Menu Create() => new Menu
        {
            Id = Id,
            PluginName = PluginName,
            Name = Name,
            IsPanelPage = IsPanelPage,
            ControllerName = ControllerName,
            ParentId = ParentId,
            ActionName = ActionName
        };
        public override IModelDto<Menu> From (Menu entity)
        {

            Id = entity.Id;
            PluginName = entity.PluginName;
            Name = entity.Name;
            IsPanelPage = entity.IsPanelPage;
            ControllerName = entity.ControllerName;
            ParentId = entity.ParentId;
            ActionName = entity.ActionName;

            return this;
        }

        public override Menu Update() => new Menu
        {
            Id = Id,
            PluginName = PluginName,
            Name = Name,
            IsPanelPage = IsPanelPage,
            ControllerName = ControllerName,
            ParentId = ParentId,
            ActionName = ActionName
        };
    }
}

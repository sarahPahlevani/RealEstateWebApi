using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.Collections.Generic;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class MenuDto : ModelDtoBase<Menu>
    {
      
        public override int Id { get; set ; }
        public string Name { get; set; }
        public bool IsPanelPage { get; set; }
        public int? ParentId { get; set; }
        public string PluginName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconName { get; set; }
        public override Menu Create() => new Menu
        {
            Id = Id,
            PluginName = PluginName,
            Name = Name,
            IsPanelPage = IsPanelPage,
            ControllerName = ControllerName,
            ParentId = ParentId,
            //IconName=IconName,
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
            //IconName = entity.IconName;
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
            //IconName = IconName,
            ActionName = ActionName
        };
    }

    public class ParentMenuDto : ModelDtoBase<Menu>
    {
        public ParentMenuDto()
        {
            subs = new List<subMenuDto>();
        }
        public override int Id { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string IconName { get; set; }
        public string ControllerName { get; set; }
        public string PluginName { get; set; }
        public string address
        {
            get
            {
                var url =  (PluginName == null ? "" : "/" + PluginName) +
                (ControllerName == null ? "" : "/" + ControllerName) +
                (ActionName == null ? "" : "/" + ActionName);
                return url;
            }
        }
        public List<subMenuDto> subs { get; set; }

        public override Menu Create()
        {
            throw new System.NotImplementedException();
        }

        public override IModelDto<Menu> From(Menu entity)
        {
            throw new System.NotImplementedException();
        }

        public override Menu Update()
        {
            throw new System.NotImplementedException();
        }
    }
    public class subMenuDto
    {
        public int Id { get; set; }
        public int parentId { get; set; }
        public string IconName { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string PluginName { get; set; }
        public string address { get 
            {
                var url =  (PluginName==null?"":"/"+PluginName)+
                ( ControllerName == null ? "" : "/" + ControllerName)+
                (ActionName == null ? "" : "/" + ActionName);
                return url;
            } }
    }
}

using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.Collections.Generic;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class ContentPageDto : ModelDtoBase<ContentPage>
    {

        public override int Id { get; set; }

        public string ContentHeader { get; set; }
        public string ContectDetail { get; set; }
        public string ContentFooter { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }
        public override ContentPage Create() => new ContentPage
        {
            Id = Id,
            ContentHeader = ContentHeader,
            ContectDetail = ContectDetail,
            ContentFooter = ContentFooter,
            Title = Title,
            MenuId = MenuId
        };
        public override IModelDto<ContentPage> From(ContentPage entity)
        {

            Id = Id;
            ContentHeader = ContentHeader;
            ContectDetail = ContectDetail;
            ContentFooter = ContentFooter;
            Title = Title;
            MenuId = MenuId;

            return this;
        }

        public override ContentPage Update() => new ContentPage
        {
            Id = Id,
            ContentHeader = ContentHeader,
            ContentFooter = ContentFooter,
            ContectDetail = ContectDetail,
            Title = Title,
            MenuId = MenuId
        };

    }
    
    public class ContentPageList : IDto
    {
        public  int Id { get; set; }
        public string ContentHeader { get; set; }
        public string ContectDetail { get; set; }
        public string ContentFooter { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }

    }

    public class ContentPage2
    {
        public int Id { get; set; }
        public string ContentHeader { get; set; }
        public string ContectDetail { get; set; }
        public string ContentFooter { get; set; }
        
        public string Title { get; set; }

    }

    public class ContentPageHeader
    {
        public int Id { get; set; }
        

        public string Title { get; set; }

    }
}

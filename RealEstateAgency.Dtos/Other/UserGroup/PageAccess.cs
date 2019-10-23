namespace RealEstateAgency.Dtos.Other.UserGroup
{
    public class PageAccess
    {
        public string Name { get; set; }
        public bool AccessToPage { get; set; } = true;
        public bool AccessCreate { get; set; } = true;
        public bool AccessGet { get; set; } = true;
        public bool AccessUpdate { get; set; } = true;
        public bool AccessDelete { get; set; } = true;
        public bool AccessModify => AccessCreate && AccessDelete && AccessUpdate;
    }
}

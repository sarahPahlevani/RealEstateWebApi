namespace RealEstateAgency.Shared.AccessAttributes
{
    public class ModifyAccessAttribute : UserAccessAttribute
    {
        public ModifyAccessAttribute(params string[] roles) : base(roles)
        {
        }
    }
}

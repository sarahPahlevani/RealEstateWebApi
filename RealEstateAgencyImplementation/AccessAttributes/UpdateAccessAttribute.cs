namespace RealEstateAgency.Shared.AccessAttributes
{
    public class UpdateAccessAttribute : UserAccessAttribute
    {
        public UpdateAccessAttribute(params string[] roles) : base(roles)
        {
        }
    }
}

namespace RealEstateAgency.Shared.AccessAttributes
{
    public class GetAccessAttribute : UserAccessAttribute
    {
        public GetAccessAttribute(params string[] roles) : base(roles)
        {
        }
    }
}

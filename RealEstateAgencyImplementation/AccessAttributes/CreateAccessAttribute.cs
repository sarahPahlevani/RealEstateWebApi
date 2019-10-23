namespace RealEstateAgency.Shared.AccessAttributes
{
    public class CreateAccessAttribute : UserAccessAttribute
    {
        public CreateAccessAttribute(params string[] roles) : base(roles)
        {
        }
    }
}

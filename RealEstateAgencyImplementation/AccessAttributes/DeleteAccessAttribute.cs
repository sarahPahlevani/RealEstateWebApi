namespace RealEstateAgency.Shared.AccessAttributes
{
    public class DeleteAccessAttribute : UserAccessAttribute
    {
        public DeleteAccessAttribute(params string[] roles) : base(roles)
        {
        }
    }
}

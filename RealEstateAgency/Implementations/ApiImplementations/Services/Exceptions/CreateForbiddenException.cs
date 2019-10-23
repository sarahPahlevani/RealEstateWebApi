namespace RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions
{
    public class CreateForbiddenException : ForbiddenException
    {
        public CreateForbiddenException() : base("The create action is forbidden")
        {
            
        }
    }
}

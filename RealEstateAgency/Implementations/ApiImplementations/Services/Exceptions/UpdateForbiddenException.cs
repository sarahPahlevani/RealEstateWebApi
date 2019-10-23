namespace RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions
{
    public class UpdateForbiddenException : ForbiddenException
    {
        public UpdateForbiddenException() : base("The update action is forbidden")
        {

        }
    }
}

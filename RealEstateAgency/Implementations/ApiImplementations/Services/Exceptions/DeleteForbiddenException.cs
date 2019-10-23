namespace RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions
{
    public class DeleteForbiddenException : ForbiddenException
    {
        public DeleteForbiddenException() : base("The delete action is forbidden")
        {

        }
    }
}

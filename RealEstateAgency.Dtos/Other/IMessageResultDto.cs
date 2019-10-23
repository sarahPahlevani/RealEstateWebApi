namespace RealEstateAgency.Dtos.Other
{
    public interface IMessageResultDto<TResponse>
    {
        TResponse Message { get; set; }
        bool Ok { get; }
        TResponse ErrorMessage { get; set; }
    }
}
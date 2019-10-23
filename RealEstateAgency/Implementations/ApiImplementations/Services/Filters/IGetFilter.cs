namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    public interface IGetFilter<TResult>
    {
        IGetFilter<TResult> Next { get; }

        IGetFilter<TResult> SetNext(IGetFilter<TResult> next);

        TResult Filter(TResult filter);

        TResult Run(TResult filter);
    }
}

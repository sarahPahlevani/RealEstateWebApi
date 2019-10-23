namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    public abstract class GetFilter<TResult> : IGetFilter<TResult>
    {
        public IGetFilter<TResult> SetNext(IGetFilter<TResult> next)
        {
            if (Next is null)
                Next = next;
            else
                Next.SetNext(next);
            return this;
        }

        public abstract TResult Filter(TResult filter);

        public TResult Run(TResult filter)
        {
            filter = Filter(filter);
            return Next is null ? filter : Next.Run(filter);
        }

        public IGetFilter<TResult> Next { get; private set; }
    }
}

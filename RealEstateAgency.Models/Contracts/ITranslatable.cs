namespace RealEstateAgency.DAL.Contracts
{
    public interface ITranslatable<out TEntity>
        where TEntity : class, IEntity
    {
        TEntity Translate(int languageId);
    }
}

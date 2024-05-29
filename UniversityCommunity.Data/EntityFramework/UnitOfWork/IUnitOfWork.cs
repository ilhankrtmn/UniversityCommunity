namespace UniversityCommunity.Data.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}

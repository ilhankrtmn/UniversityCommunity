namespace UniversityCommunity.Data.EntityFramework.UnitOfWork
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly UniversityCommunityContext _context;

        public EfCoreUnitOfWork(UniversityCommunityContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}

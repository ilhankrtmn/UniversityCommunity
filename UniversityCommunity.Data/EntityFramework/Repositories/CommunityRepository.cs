using Microsoft.EntityFrameworkCore;
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class CommunityRepository : EfCoreRepositoryBase<Community>, ICommunityRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public CommunityRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Community>> GetAllCommunityAsync()
        {
            return await _context.Communities.Where(p => p.Status == true).ToListAsync();
        }
    }
}

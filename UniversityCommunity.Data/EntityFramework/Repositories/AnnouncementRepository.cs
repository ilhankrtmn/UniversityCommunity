using Microsoft.EntityFrameworkCore;
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class AnnouncementRepository : EfCoreRepositoryBase<Announcement>, IAnnouncementRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public AnnouncementRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Announcement>> GetAllAnnouncementAsync()
        {
            return await _context.Announcements.Where(p => p.Status == true).ToListAsync();
        }
    }
}

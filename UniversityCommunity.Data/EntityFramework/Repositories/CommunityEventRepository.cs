using Microsoft.EntityFrameworkCore;
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class CommunityEventRepository : EfCoreRepositoryBase<CommunityEvent>, ICommunityEventRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public CommunityEventRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EventType>> GetEventTypesAsync()
        {
            return await _context.EventTypes.ToListAsync();
        }

        public async Task<List<CommunityEvent>> GetCommunityEventListAsync(GetCommunityEventListRequestDto requestDto)
        {
            if (requestDto.Status == 1)
            {
                return await _context.CommunityEvents.Include(p => p.Community)
                .Where(p => p.Status == requestDto.Status)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
            }
            else if (requestDto.Status == 0)
            {
                return await _context.CommunityEvents.Include(p => p.Community)
                .Where(p => p.Community.AdvisorId == requestDto.UserId && p.Status == requestDto.Status)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
            }
            else
            {
                return await _context.CommunityEvents.Include(p => p.Community)
               .Where(p => p.UserId == requestDto.UserId)
               .OrderByDescending(p => p.CreatedDate)
               .ToListAsync();
            }
        }
    }
}

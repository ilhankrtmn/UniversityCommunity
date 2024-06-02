using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Data.EntityFramework.Repositories.Interfaces
{
    public interface ICommunityEventRepository : IRepositoryBase<CommunityEvent>
    {
        Task<List<EventType>> GetEventTypesAsync();
        Task<List<CommunityEvent>> GetCommunityEventListAsync(GetCommunityEventListRequestDto requestDto);
    }
}

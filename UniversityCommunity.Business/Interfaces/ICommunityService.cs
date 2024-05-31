using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityService
    {
        Task<List<Community>> GetCommunityListAsync();
        Task<List<Community>> GetAdminAnnouncementListAsync();
        Task<Community> GetCommunityAsync(int communityId);
        Task SaveorUpdateCommunity(Community community);
        Task<bool> DeleteCommunity(int communityId);
    }
}

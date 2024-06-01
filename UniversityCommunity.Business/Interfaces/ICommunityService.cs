using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityService
    {
        Task<List<Community>> GetCommunityListAsync();
        Task<List<Community>> GetAdminAnnouncementListAsync();
        Task<Community> GetCommunityAsync(int communityId);
        Task SaveorUpdateCommunity(Community community);
        Task<bool> DeleteCommunity(int communityId);
        Task<bool> SaveCommunityMember(CommunityforPage requestDto);
    }
}

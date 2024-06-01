using System.Web.Mvc;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityService
    {
        Task<List<Community>> GetCommunityListAsync();
        Task<List<Community>> GetAdminAnnouncementListAsync();
        Task<Community> GetCommunityAsync(int communityId);
        Task SaveorUpdateCommunity(Community community);
        Task<bool> DeleteCommunity(int communityId);
        Task<List<SelectListItem>> GetCommunityUserList(int userTypeId);
        Task<string> CommunityUserAccount(CommunityUserAccountRequestDto requestDto);
    }
}

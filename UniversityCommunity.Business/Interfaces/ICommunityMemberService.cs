using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityMemberService
    {
        Task<bool> SaveCommunityMember(CommunityforPage requestDto);
        Task<string> CommunityAdvisorAccount(int communityId);
        Task<string> CommunityLeaderAccount(int communityId);
        Task<List<string>> CommunityMembers(int communityId);
    }
}
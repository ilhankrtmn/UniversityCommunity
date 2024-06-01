using UniversityCommunity.Data.Models;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityMemberService
    {
        Task<bool> SaveCommunityMember(CommunityforPage requestDto);
        Task<List<string>> CommunityMembers(int communityId);
    }
}
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Data.EntityFramework.Repositories.Interfaces
{
    public interface ICommunityMemberRepository : IRepositoryBase<CommunityMember>
    {
        bool CheckCommunityMember(CheckCommunityMemberRequestDto requestDto);
    }
}

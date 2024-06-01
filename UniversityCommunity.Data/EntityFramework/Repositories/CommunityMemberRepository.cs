using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class CommunityMemberRepository : EfCoreRepositoryBase<CommunityMember>, ICommunityMemberRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public CommunityMemberRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckCommunityMember(CheckCommunityMemberRequestDto requestDto)
        {
            return _context.CommunityMembers.Where(p => p.CommunityId == requestDto.CommunityId && p.Email == requestDto.Email).Any();
        }
    }
}

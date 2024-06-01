using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;
using UniversityCommunity.Data.Models.PageModel;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Services
{
    public class CommunityMemberService : ICommunityMemberService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityMemberRepository _communityMemberRepository;

        public CommunityMemberService(IUnitOfWork unitOfWork, ICommunityMemberRepository communityMemberRepository)
        {
            _unitOfWork = unitOfWork;
            _communityMemberRepository = communityMemberRepository;
        }

        public async Task<bool> SaveCommunityMember(CommunityforPage requestDto)
        {
            requestDto.CommunityMember.CommunityId = requestDto.Community.Id;
            var communityMember = _communityMemberRepository.CheckCommunityMember(new CheckCommunityMemberRequestDto
            {
                CommunityId = requestDto.CommunityMember.CommunityId,
                Email = requestDto.CommunityMember.Email
            });
            if (communityMember)
            {
                return false;
            }
            else
            {
                requestDto.CommunityMember.UserTypeId = 4;
                _communityMemberRepository.Add(requestDto.CommunityMember);
                await _unitOfWork.CompleteAsync();
                return true;
            }
        }

        public async Task<string> CommunityAdvisorAccount(int communityId)
        {
            return (await _communityMemberRepository.FindListAsync(p => p.UserTypeId == 2 && p.CommunityId == communityId))
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => $"{p.Name} {p.Surname}")
                .FirstOrDefault();
        }

        public async Task<string> CommunityLeaderAccount(int communityId)
        {
            return (await _communityMemberRepository.FindListAsync(p => p.UserTypeId == 3 && p.CommunityId == communityId))
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => $"{p.Name} {p.Surname}")
                .FirstOrDefault();
        }

        public async Task<List<string>> CommunityMembers(int communityId)
        {
            return (await _communityMemberRepository.FindListAsync(p => p.UserTypeId == 4 && p.CommunityId == communityId))
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => $"{p.Name} {p.Surname}")
                .ToList();
        }

    }
}

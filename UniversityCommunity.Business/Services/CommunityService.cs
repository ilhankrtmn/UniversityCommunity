using System.Web.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Services
{
    public class CommunityService : ICommunityService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityMemberRepository _communityMemberRepository;
        private readonly IUserRepository _userRepository;

        public CommunityService(IUnitOfWork unitOfWork, ICommunityRepository communityRepository, ICommunityMemberRepository communityMemberRepository
            , IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _communityRepository = communityRepository;
            _communityMemberRepository = communityMemberRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Community>> GetCommunityListAsync()
        {
            return (await _communityRepository.FindListAsync(p => p.Status == true)).OrderByDescending(p => p.CreatedDate).ToList();

        }

        public async Task<List<Community>> GetAdminAnnouncementListAsync()
        {
            return (await _communityRepository.GetAllAsync()).OrderByDescending(p => p.CreatedDate).ToList();
        }

        public async Task<Community> GetCommunityAsync(int communityId)
        {
            return await _communityRepository.FindAsync(p => p.Id == communityId);
        }

        public async Task SaveorUpdateCommunity(Community community)
        {
            _communityRepository.AddOrUpdate(community);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteCommunity(int communityId)
        {
            var community = await _communityRepository.FindAsync(p => p.Id == communityId);
            if (community != null)
            {
                _communityRepository.Delete(community);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<List<SelectListItem>> GetCommunityUserList(int userTypeId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = (await _userRepository.FindListAsync(p => p.UserTypeId == userTypeId && p.Status == true)).ToList();

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = $"{item.Name} {item.Surname}" });
            }

            return items;
        }

        public async Task<string> CommunityUserAccount(CommunityUserAccountRequestDto requestDto)
        {
            var user = await _userRepository.FindAsync(p => p.UserTypeId == requestDto.UserTypeId && p.Id == requestDto.UserId);

            return (user != null) ? $"{user.Name} {user.Surname}" : string.Empty;
        }
    }
}

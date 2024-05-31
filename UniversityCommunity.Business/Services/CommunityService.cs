using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;

namespace UniversityCommunity.Business.Services
{
    public class CommunityService : ICommunityService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityRepository _communityRepository;

        public CommunityService(IUnitOfWork unitOfWork, ICommunityRepository communityRepository)
        {
            _unitOfWork = unitOfWork;
            _communityRepository = communityRepository;
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
    }
}

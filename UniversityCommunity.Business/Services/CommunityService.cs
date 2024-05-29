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

        public async Task<List<Community>> GetCommunityAsync()
        {
            return await _communityRepository.GetAllCommunityAsync();
        }
    }
}

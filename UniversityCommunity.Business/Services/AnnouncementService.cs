using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;

namespace UniversityCommunity.Business.Services
{
    public class AnnouncementService : IAnnouncementService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IUnitOfWork unitOfWork, IAnnouncementRepository announcementRepository)
        {
            _unitOfWork = unitOfWork;
            _announcementRepository = announcementRepository;
        }

        public async Task<List<Announcement>> GetAnnouncementAsync()
        {
            return await _announcementRepository.GetAllAnnouncementAsync();
        }
    }
}

using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
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

        public async Task<List<Announcement>> GetAnnouncementListAsync()
        {
            return (await _announcementRepository.FindListAsync(p => p.Status == true)).OrderByDescending(p => p.CreatedDate).ToList();
        }

        public async Task<List<Announcement>> GetAdminAnnouncementListAsync()
        {
            return (await _announcementRepository.GetAllAsync()).OrderByDescending(p => p.CreatedDate).ToList();
        }

        public async Task<Announcement> GetAnnouncementAsync(int announcementId)
        {
            return await _announcementRepository.FindAsync(p => p.Id == announcementId);
        }

        public async Task SaveorUpdate(Announcement announcement)
        {
            _announcementRepository.AddOrUpdate(announcement);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteAnnouncement(int announcementId)
        {
            var announcement = await _announcementRepository.FindAsync(p => p.Id == announcementId);
            if (announcement != null)
            {
                _announcementRepository.Delete(announcement);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}

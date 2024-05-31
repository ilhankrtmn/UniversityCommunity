using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetAnnouncementListAsync();
        Task<List<Announcement>> GetAdminAnnouncementListAsync();
        Task<Announcement> GetAnnouncementAsync(int announcementId);
        Task SaveorUpdate(Announcement announcement);
        Task<bool> DeleteAnnouncement(int announcementId);
    }
}

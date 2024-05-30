using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetAnnouncementListAsync();
        Task<Announcement> GetAnnouncementAsync(int announcementId);
    }
}

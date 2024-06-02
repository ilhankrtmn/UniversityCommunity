using System.Web.Mvc;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetUserListAsync();
        Task<User> GetUserAsync(int userId);
        Task SaveorUpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<List<SelectListItem>> GetCommunityEventTypeList();
        Task<List<SelectListItem>> GetLeaderCommunityList(int userId);
    }
}

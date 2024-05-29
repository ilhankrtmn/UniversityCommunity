using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.EntityFramework.Repositories.Interfaces
{
    public interface IOutgoingMailRepository : IRepositoryBase<OutgoingMail>
    {
        Task InsertUserEmailOtpAsync(int UserId, int pincode);
        Task SendOtpMailAsync(int userId = 0, string email = "", string message = "");
        Task<int> CheckUserEmailIdAsync(int userId, int pincode);
        Task UpdateUserEmailOtpAsync(int emailId);
    }
}

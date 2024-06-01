using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;

namespace UniversityCommunity.Business.Services
{
    public class AdminService : IAdminService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public AdminService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUserListAsync()
        {
            return (await _userRepository.FindListAsync(p => p.UserTypeId == 2 || p.UserTypeId == 3)).OrderByDescending(p => p.CreatedDate).ToList();
        }
        public async Task<User> GetUserAsync(int userId)
        {
            return await _userRepository.FindAsync(p => p.Id == userId);
        }

        public async Task SaveorUpdateUser(User user)
        {
            user.UpdatedDate = DateTime.Now;
            _userRepository.AddOrUpdate(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _userRepository.FindAsync(p => p.Id == userId);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}

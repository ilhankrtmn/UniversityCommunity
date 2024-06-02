using System.Web.Mvc;
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
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityEventRepository _communityEventRepository;

        public AdminService(IUnitOfWork unitOfWork, IUserRepository userRepository, ICommunityRepository communityRepository, ICommunityEventRepository communityEventRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _communityRepository = communityRepository;
            _communityEventRepository = communityEventRepository;
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

        public async Task<List<SelectListItem>> GetCommunityEventTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = await _communityEventRepository.GetEventTypesAsync();

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = item.Name });
            }

            return items;
        }

        public async Task<List<SelectListItem>> GetLeaderCommunityList(int userId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = await _communityRepository.FindListAsync(p => p.LeaderId == userId);

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = item.Title });
            }

            return items;
        }
    }
}

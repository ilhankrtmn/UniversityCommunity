using System.Web.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;
using UniversityCommunity.Data.Enums;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Services
{
    public class CommunityService : ICommunityService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommunityEventRepository _communityEventRepository;

        public CommunityService(IUnitOfWork unitOfWork, ICommunityRepository communityRepository, IUserRepository userRepository
            , ICommunityEventRepository communityEventRepository)
        {
            _unitOfWork = unitOfWork;
            _communityRepository = communityRepository;
            _userRepository = userRepository;
            _communityEventRepository = communityEventRepository;
        }

        public async Task<List<Community>> GetCommunityListAsync()
        {
            return (await _communityRepository.FindListAsync(p => p.Status == true)).OrderByDescending(p => p.CreatedDate).ToList();

        }

        public async Task<List<Community>> GetAdminCommunityListAsync()
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

        public async Task<List<SelectListItem>> GetCommunityUserList(int userTypeId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = (await _userRepository.FindListAsync(p => p.UserTypeId == userTypeId && p.Status == true)).ToList();

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = $"{item.Name} {item.Surname}" });
            }

            return items;
        }

        public async Task<string> CommunityUserAccount(CommunityUserAccountRequestDto requestDto)
        {
            var user = await _userRepository.FindAsync(p => p.UserTypeId == requestDto.UserTypeId && p.Id == requestDto.UserId);

            return (user != null) ? $"{user.Name} {user.Surname}" : string.Empty;
        }

        public async Task<List<CommunityEvent>> GetCommunityEventListAsync(int userId)
        {
            int status = 0;
            var user = await _userRepository.FindAsync(p => p.Id == userId);
            if (user != null)
            {
                if (user.UserTypeId == (int)UserTypes.Admin)
                {
                    status = 1;

                    return await _communityEventRepository.GetCommunityEventListAsync(new GetCommunityEventListRequestDto
                    {
                        UserId = 0,
                        Status = status
                    });
                }
                else if (user.UserTypeId == (int)UserTypes.Advisor)
                {
                    status = 0;

                    return await _communityEventRepository.GetCommunityEventListAsync(new GetCommunityEventListRequestDto
                    {
                        UserId = userId,
                        Status = status
                    });
                }
                else if (user.UserTypeId == (int)UserTypes.Leader)
                {
                    status = 2;

                    return await _communityEventRepository.GetCommunityEventListAsync(new GetCommunityEventListRequestDto
                    {
                        UserId = userId,
                        Status = status
                    });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<CommunityEvent> GetCommunityEventAsync(int communityEventId)
        {
            return await _communityEventRepository.FindAsync(p => p.Id == communityEventId);
        }

        public async Task SaveorUpdateCommunityEvent(CommunityEvent communityEvent)
        {
            communityEvent.UpdatedDate = DateTime.Now;
            _communityEventRepository.AddOrUpdate(communityEvent);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ConfirmRejectEventAsync(ConfirmRejectEventRequestDto requestDto)
        {
            int status = 0;
            status = requestDto.Status switch
            {
                1 or 2 => 0,
                3 or 4 => 1,
                0 => status
            };

            var communityEvent = await _communityEventRepository.FindAsync(p => p.Id == requestDto.EventId && p.Status == status);
            if (communityEvent != null)
            {
                communityEvent.Status = requestDto.Status;
                communityEvent.UpdatedDate = DateTime.Now;
                _communityEventRepository.Update(communityEvent);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            return false;
        }
    }
}

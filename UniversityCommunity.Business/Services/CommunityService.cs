﻿using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;
using UniversityCommunity.Data.Models;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.Business.Services
{
    public class CommunityService : ICommunityService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityMemberRepository _communityMemberRepository;

        public CommunityService(IUnitOfWork unitOfWork, ICommunityRepository communityRepository, ICommunityMemberRepository communityMemberRepository
            )
        {
            _unitOfWork = unitOfWork;
            _communityRepository = communityRepository;
            _communityMemberRepository = communityMemberRepository;
        }

        public async Task<List<Community>> GetCommunityListAsync()
        {
            return (await _communityRepository.FindListAsync(p => p.Status == true)).OrderByDescending(p => p.CreatedDate).ToList();

        }

        public async Task<List<Community>> GetAdminAnnouncementListAsync()
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

        public async Task<bool> SaveCommunityMember(CommunityforPage requestDto)
        {
            requestDto.CommunityMember.CommunityId = requestDto.Community.Id;
            var communityMember = _communityMemberRepository.CheckCommunityMember(new CheckCommunityMemberRequestDto
            {
                CommunityId = requestDto.CommunityMember.CommunityId,
                Email = requestDto.CommunityMember.Email
            });
            if (communityMember)
            {
                return false;
            }
            else
            {
                _communityMemberRepository.Add(requestDto.CommunityMember);
                await _unitOfWork.CompleteAsync();
                return true;
            }
        }
    }
}

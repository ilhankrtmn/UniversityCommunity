﻿using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.Interfaces
{
    public interface ICommunityService
    {
        Task<List<Community>> GetCommunityListAsync();
        Task<Community> GetCommunityAsync(int communityId);
    }
}

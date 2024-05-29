using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class UserRepository : EfCoreRepositoryBase<User>, IUserRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public UserRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }
    }
}

using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.Models.PageModel
{
    public class CommunityforListPage
    {
        public List<Community> Communities { get; set; }
    }

    public class CommunityforPage
    {
        public Community Community { get; set; }
        public CommunityMember CommunityMember { get; set; }
    }
}

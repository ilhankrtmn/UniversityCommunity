using System.Web.Mvc;
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
        public string CommunityAdvisorAccount { get; set; }
        public string CommunityLeaderAccount { get; set; }
        public List<string> CommunityMembers { get; set; }


        public IEnumerable<SelectListItem> AdvisorList { get; set; }
        public IEnumerable<SelectListItem> LeaderList { get; set; }
    }
}

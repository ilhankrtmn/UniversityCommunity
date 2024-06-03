using System.Web.Mvc;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.Models.PageModel
{
    public class CommunityEventforListPage
    {
        public List<CommunityEvent> CommunityEvents { get; set; }
    }

    public class CommunityEventforPage
    {
        public CommunityEvent CommunityEvent { get; set; }
        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public IEnumerable<SelectListItem> Communities { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.App.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ICommunityService _communityService;

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }

        [HttpGet]
        public async Task<IActionResult> CommunityList()
        {
            CommunityforListPage communityforListPage = new CommunityforListPage();

            communityforListPage.Communities = await _communityService.GetCommunityListAsync();

            return View(communityforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> CommunityDetail(int communityId)
        {
            //TODO Bu endpoint view kısmında yapılacak işlemler var.
            CommunityforPage communityforPage = new CommunityforPage();

            communityforPage.Community = await _communityService.GetCommunityAsync(communityId);

            return View(communityforPage);
        }
    }
}

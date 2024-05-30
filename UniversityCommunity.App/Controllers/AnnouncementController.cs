using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Business.Services;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.App.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> AnnouncementList()
        {
            AnnouncementforListPage announcementforListPage = new AnnouncementforListPage();

            announcementforListPage.Announcements = await _announcementService.GetAnnouncementListAsync();

            return View(announcementforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> AnnouncementDetail(int announcementId)
        {
            AnnouncementforPage announcementforPage = new AnnouncementforPage();

            announcementforPage.Announcement = await _announcementService.GetAnnouncementAsync(announcementId);

            return View(announcementforPage);
        }
    }
}

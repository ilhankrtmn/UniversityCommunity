using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.EntityFramework.Entities;
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
        public async Task<IActionResult> AdminAnnouncementList()
        {
            AnnouncementforListPage announcementforListPage = new AnnouncementforListPage();

            announcementforListPage.Announcements = await _announcementService.GetAdminAnnouncementListAsync();

            return View(announcementforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> AnnouncementDetail(int announcementId)
        {
            AnnouncementforPage announcementforPage = new AnnouncementforPage();

            announcementforPage.Announcement = await _announcementService.GetAnnouncementAsync(announcementId);

            return View(announcementforPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveAnnouncement(int announcementId)
        {
            Announcement announcement = new Announcement();
            announcement = await _announcementService.GetAnnouncementAsync(announcementId);
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAnnouncement(Announcement announcement)
        {
            await _announcementService.SaveorUpdate(announcement);
            return View(announcement);
        }

        [HttpPost]
        public async Task<bool> DeleteAnnouncement(int announcementId)
        {
            return await _announcementService.DeleteAnnouncement(announcementId);
        }
    }
}

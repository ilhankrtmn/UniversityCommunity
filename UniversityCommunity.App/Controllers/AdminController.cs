using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Business.ValidationRules;
using UniversityCommunity.Data.Models;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ICommunityService _communityService;

        public AdminController(IAdminService adminService, ICommunityService communityService)
        {
            _adminService = adminService;
            _communityService = communityService;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            UserforListPage userforListPage = new UserforListPage();

            userforListPage.Users = await _adminService.GetUserListAsync();

            return View(userforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveUser(int userId)
        {
            UserforPage userforPage = new UserforPage();
            List<UserforPage> SearchList = new List<UserforPage>
                {
                 new UserforPage { UserTypeId = 2, UserTypeName = "Danışman Hoca" },
                 new UserforPage { UserTypeId = 3, UserTypeName = "Topluluk Lideri" }
                };

            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            userforPage.User = await _adminService.GetUserAsync(userId);
            return View(userforPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserforPage userforPage)
        {
            UserValidator validationRules = new UserValidator();
            ValidationResult validationResult = validationRules.Validate(userforPage.User);

            List<UserforPage> SearchList = new List<UserforPage>
                {
                 new UserforPage { UserTypeId = 2, UserTypeName = "Danışman Hoca" },
                 new UserforPage { UserTypeId = 3, UserTypeName = "Topluluk Lideri" }
                };

            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            if (validationResult.IsValid)
            {
                await _adminService.SaveorUpdateUser(userforPage.User);

                return RedirectToAction("UserList", "Admin");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ViewBag.ValidateControl = item.ErrorMessage;
                }

                return View(userforPage);
            }
        }

        [HttpPost]
        public async Task<bool> DeleteUser(int userId)
        {
            return await _adminService.DeleteUser(userId);
        }

        [HttpGet]
        public async Task<IActionResult> AdminCommunityEventList()
        {
            CommunityEventforListPage communityEventforListPage = new CommunityEventforListPage();
            /* 
             * TODO Leader olan kullanıcı için detayları görüntüleme sayfası olmamalı
             * Leader dışındaki diğer kullanıcılar için de edit kısmını görmemesi lazım.
             */

            // TODO HttpContext'ten oku.
            int userId = 2;
            communityEventforListPage.CommunityEvents = await _communityService.GetCommunityEventListAsync(userId);

            if (communityEventforListPage.CommunityEvents != null)
            {
                return View(communityEventforListPage);
            }
            else
            {
                return RedirectToAction("CommunityList", "Community");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CommunityEventDetail(int communityEventId)
        {
            CommunityEventforPage communityEventforPage = new CommunityEventforPage();
            // TODO HttpContext'ten oku.
            int userId = 2;

            communityEventforPage.CommunityEvent = await _communityService.GetCommunityEventAsync(communityEventId);
            communityEventforPage.EventTypes = await _adminService.GetCommunityEventTypeList();
            communityEventforPage.Communities = await _adminService.GetLeaderCommunityList(userId);

            return View(communityEventforPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveCommunityEvent(int communityEventId)
        {
            CommunityEventforPage communityEventforPage = new CommunityEventforPage();
            // TODO HttpContext'ten oku.
            int userId = 2;

            communityEventforPage.CommunityEvent = await _communityService.GetCommunityEventAsync(communityEventId);
            communityEventforPage.EventTypes = await _adminService.GetCommunityEventTypeList();
            communityEventforPage.Communities = await _adminService.GetLeaderCommunityList(userId);

            return View(communityEventforPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCommunityEvent(CommunityEventforPage communityEventforPage)
        {
            // TODO HttpContext'ten oku.
            int userId = 2;
            communityEventforPage.CommunityEvent.UserId = userId;
            await _communityService.SaveorUpdateCommunityEvent(communityEventforPage.CommunityEvent);

            communityEventforPage.EventTypes = await _adminService.GetCommunityEventTypeList();
            communityEventforPage.Communities = await _adminService.GetLeaderCommunityList(userId);

            return View(communityEventforPage);
        }


        [HttpPost]
        public async Task<bool> ConfirmRejectEvent(int eventId, int status)
        {
            /*
             * TODO burada status değeri adminse 3 veya 4 gelirken danışman hoca ise 1 veya 2 olmalı
             * Dilersen view'den gelirken değiştir. Dilersen de userId'den ya da userTypeId'ye göre manuel setle
            */
            return await _communityService.ConfirmRejectEventAsync(new ConfirmRejectEventRequestDto
            {
                EventId = eventId,
                Status = status,
            });
        }
    }
}
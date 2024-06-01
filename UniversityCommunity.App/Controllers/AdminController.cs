using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Business.ValidationRules;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
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
            //UserType kısmını yap.
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
    }
}

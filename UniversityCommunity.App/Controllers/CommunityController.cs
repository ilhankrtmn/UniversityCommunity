using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Business.ValidationRules;
using UniversityCommunity.Data.EntityFramework.Entities;
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
        public async Task<IActionResult> AdminCommunityList()
        {
            CommunityforListPage communityforListPage = new CommunityforListPage();

            communityforListPage.Communities = await _communityService.GetAdminAnnouncementListAsync();

            return View(communityforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> CommunityDetail(int communityId)
        {
            //TODO Bu endpoint view kısmında yapılacak işlemler var.
            CommunityforPage communityforPage = new CommunityforPage();

            communityforPage.Community = await _communityService.GetCommunityAsync(communityId);
            ViewBag.ValidateControl = HttpContext.Session.GetString("ValidateControl");

            return View(communityforPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveCommunity(int communityId)
        {
            Community community = new Community();
            community = await _communityService.GetCommunityAsync(communityId);

            return View(community);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCommunity(Community community)
        {
            await _communityService.SaveorUpdateCommunity(community);
            return RedirectToAction("CommunityList", "Community");
        }

        [HttpPost]
        public async Task<bool> DeleteCommunity(int communityId)
        {
            return await _communityService.DeleteCommunity(communityId);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCommunityMember(CommunityforPage requestDto)
        {
            CommunityMemberValidator validationRules = new CommunityMemberValidator();
            ValidationResult validationResult = validationRules.Validate(requestDto.CommunityMember);

            if (validationResult.IsValid)
            {
                bool result = await _communityService.SaveCommunityMember(requestDto);

                if (result)
                {
                    HttpContext.Session.Remove("ValidateControl");
                }
                else
                {
                    HttpContext.Session.SetString("ValidateControl", "Bu email ile kayıt zaten mevcuttur.");
                }
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    HttpContext.Session.SetString("ValidateControl", item.ErrorMessage);
                }
            }

            return RedirectToAction("CommunityDetail", "Community", new { communityId = requestDto.Community.Id });
        }
    }
}

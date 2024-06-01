using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Business.ValidationRules;
using UniversityCommunity.Data.Models;
using UniversityCommunity.Data.Models.PageModel;

namespace UniversityCommunity.App.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly ICommunityMemberService _communityMemberService;

        public CommunityController(ICommunityService communityService, ICommunityMemberService communityMemberService)
        {
            _communityService = communityService;
            _communityMemberService = communityMemberService;
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
            CommunityforPage communityforPage = new CommunityforPage();

            communityforPage.Community = await _communityService.GetCommunityAsync(communityId);

            communityforPage.CommunityAdvisorAccount = await _communityService.CommunityUserAccount(new CommunityUserAccountRequestDto
            {
                UserTypeId = 2,
                UserId = communityforPage.Community.AdvisorId,
            });

            communityforPage.CommunityLeaderAccount = await _communityService.CommunityUserAccount(new CommunityUserAccountRequestDto
            {
                UserTypeId = 3,
                UserId = communityforPage.Community.LeaderId,
            });

            communityforPage.CommunityMembers = await _communityMemberService.CommunityMembers(communityId);

            ViewBag.ValidateControl = HttpContext.Session.GetString("ValidateControl");

            return View(communityforPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveCommunity(int communityId)
        {
            CommunityforPage communityforPage = new CommunityforPage();

            communityforPage.Community = await _communityService.GetCommunityAsync(communityId);
            communityforPage.AdvisorList = await _communityService.GetCommunityUserList(2);
            communityforPage.LeaderList = await _communityService.GetCommunityUserList(3);

            return View(communityforPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCommunity(CommunityforPage communityforPage)
        {
            CommunityValidator validationRules = new CommunityValidator();
            ValidationResult validationResult = validationRules.Validate(communityforPage.Community);

            if (validationResult.IsValid)
            {
                await _communityService.SaveorUpdateCommunity(communityforPage.Community);
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ViewBag.ValidateControl = item.ErrorMessage;
                }
            }

            communityforPage.AdvisorList = await _communityService.GetCommunityUserList(2);
            communityforPage.LeaderList = await _communityService.GetCommunityUserList(3);

            return View(communityforPage);
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
                bool result = await _communityMemberService.SaveCommunityMember(requestDto);

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

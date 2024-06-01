using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityCommunity.App.Models;
using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.App.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            //UserRegisterBody customerRegisterBody = new UserRegisterBody();

            //customerRegisterBody.CityList = await _registerService.GetCitiesAsync();
            //return View(customerRegisterBody);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterBody customerRegisterBody)
        {
            //Customer customer = new Customer();
            //customer.Name = customerRegisterBody.Name;
            //customer.Surname = customerRegisterBody.Surname;
            //customer.Email = customerRegisterBody.Email;
            //customer.CityID = customerRegisterBody.CityID;
            //customer.Password = customerRegisterBody.Password;

            //customerRegisterBody.CityList = await _registerService.GetCitiesAsync();

            //RegisterValidator registerValidator = new RegisterValidator();
            //ValidationResult validationResult = registerValidator.Validate(customer);

            //if (validationResult.IsValid)
            //{
            //    if (await _registerService.CheckRegisterMail(customerRegisterBody.Email))
            //    {
            //        await _registerService.InsertCustomer(customer);
            //        return RedirectToAction("GameList", "Customer");
            //    }
            //    else
            //    {
            //        return RedirectToAction("Login", "Customer");
            //    }
            //}
            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            //return View(customerRegisterBody);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginBody userLoginBody)
        {
            var Id = await _authenticationService.CheckCustomerLogin(userLoginBody.Email, userLoginBody.Password);
            if (Id != 0)
            {
                HttpContext.Session.SetString("Id", Id.ToString());
                return RedirectToAction("Register", "Authentication");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SendOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(SendOtpRequestDto requestDto)
        {
            var response = await _authenticationService.SendOtp(requestDto);
            return (response == true) ? RedirectToAction("CheckOtp", "Authentication") : RedirectToAction("SendOtp", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> CheckOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckOtp(CheckOtpRequestDto requestDto)
        {
            var response = await _authenticationService.CheckOtp(requestDto);

            return (response == true) ? RedirectToAction("ResetPassword", "Authentication") : RedirectToAction("CheckOtp", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto requestDto)
        {
            var response = await _authenticationService.ResetPassword(requestDto);

            return (response == true) ? RedirectToAction("Login", "Authentication") : RedirectToAction("ResetPassword", "Authentication");
        }

    }
}

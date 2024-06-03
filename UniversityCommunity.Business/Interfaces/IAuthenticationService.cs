using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<int> CheckCustomerLogin(string email, string password);
        Task<bool> SendOtp(SendOtpRequestDto requestDto);
        Task<bool> CheckOtp(CheckOtpRequestDto requestDto);
        Task<bool> ResetPassword(ResetPasswordRequestDto requestDto);
        Task<User> GetUser(string email);
        Task<User> GetUser(int Id);
    }
}
﻿using UniversityCommunity.Business.Interfaces;
using UniversityCommunity.Data;
using User = UniversityCommunity.Data.EntityFramework.Entities.User;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;
using UniversityCommunity.Data.Models;

namespace UniversityCommunity.Business.Services
{
    public class AuthenticationService : IAuthenticationService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IOutgoingMailRepository _outgoingMailRepository;
        private readonly IEmailService _emailService;

        public AuthenticationService(IUnitOfWork unitOfWork, IOutgoingMailRepository outgoingMailRepository,
            IEmailService emailService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _outgoingMailRepository = outgoingMailRepository;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<int> CheckCustomerLogin(string email, string password)
        {
            User user = await _userRepository.FindAsNoTrackingAsync(p => p.Email == email && p.Password == password);
            return (user == null) ? 0 : user.Id;
        }

        public async Task<bool> SendOtp(SendOtpRequestDto requestDto)
        {
            User user = await _userRepository.FindAsync(p => p.Email == requestDto.Email);
            if (user == null)
            {
                return false;
            }

            var pincode = 0;
            var message = "#Pincode# Doğrulama kodu ile şifreni değiştirebilirsin.";

            pincode = Convert.ToInt32(1000.00 + (9000.00 * Functions.RandomNumber()));

            await _outgoingMailRepository.InsertUserEmailOtpAsync(user.Id, pincode);
            message = message.Replace("#Pincode#", $"{pincode}");

            _emailService.SendMail(requestDto.Email, "Şifre Değiştirme Doğrulaması", message);
            await _outgoingMailRepository.SendOtpMailAsync(user.Id, requestDto.Email, message);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> CheckOtp(CheckOtpRequestDto requestDto)
        {
            User user = await _userRepository.FindAsync(p => p.Email == requestDto.Email);
            if (user == null)
            {
                return false;
            }

            var entryID = await _outgoingMailRepository.CheckUserEmailIdAsync(user.Id, requestDto.Pincode);
            if (entryID > 0)
            {
                await _outgoingMailRepository.UpdateUserEmailOtpAsync(Convert.ToInt32(entryID));
                return true;
            }

            return false;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequestDto requestDto)
        {
            User user = await _userRepository.FindAsync(p => p.Id == requestDto.UserId);
            if (user == null)
            {
                return false;
            }

            user.Password = requestDto.Password;
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.FindAsNoTrackingAsync(p => p.Email == email);
        }
        public async Task<User> GetUser(int Id)
        {
            return await _userRepository.FindAsNoTrackingAsync(p => p.Id == Id);
        }
    }
}
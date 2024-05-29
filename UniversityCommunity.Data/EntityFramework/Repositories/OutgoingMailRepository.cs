using Microsoft.EntityFrameworkCore;
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class OutgoingMailRepository(UniversityCommunityContext context) : EfCoreRepositoryBase<OutgoingMail>(context), IOutgoingMailRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context = context;

        public async Task InsertUserEmailOtpAsync(int UserId, int pincode)
        {
            await _context.UserEmailOtps.AddAsync(new UserEmailOtp
            {
                UserId = UserId,
                Pincode = pincode
            });
            await _context.SaveChangesAsync();
        }

        public async Task SendOtpMailAsync(int userId = 0, string email = "", string message = "")
        {
            var dateThreshold = DateTime.Now.AddMinutes(-60);

            var outgoingId = await _context.OutgoingMails
                .Where(p => p.UserId == userId
                    && p.Email == email
                    && p.Message == message
                    && p.CreatedDate >= dateThreshold)
                .Select(p => (long?)p.Id)
                .SingleOrDefaultAsync();

            var outgoingMail = outgoingId ?? 0L;

            if (outgoingMail == 0)
            {
                await _context.OutgoingMails.AddAsync(new OutgoingMail
                {
                    UserId = userId,
                    Email = email,
                    Message = message,
                    Status = 0
                });
            }
        }

        public async Task<int> CheckUserEmailIdAsync(int userId, int pincode)
        {
            var userEmailOtp = await _context.UserEmailOtps.Where(p => p.UserId == userId && p.Pincode == pincode && p.CreatedDate > DateTime.Now.AddMinutes(-2))
                                                        .OrderByDescending(p => p.Id)
                                                        .FirstOrDefaultAsync();

            return userEmailOtp != null ? userEmailOtp.Id : 0;
        }

        public async Task UpdateUserEmailOtpAsync(int emailId)
        {
            var userEmailOtp = await _context.UserEmailOtps.Where(p => p.Id == emailId).SingleOrDefaultAsync();
            userEmailOtp.Status = 1;
            userEmailOtp.UpdatedDate = DateTime.Now;
            _context.UserEmailOtps.Update(userEmailOtp);


            _context.SaveChanges();
        }
    }
}

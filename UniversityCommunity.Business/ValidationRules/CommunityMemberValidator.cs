using FluentValidation;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.ValidationRules
{
    public class CommunityMemberValidator : AbstractValidator<CommunityMember>
    {
        public CommunityMemberValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Şifre kısmı boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .Matches(@"@samsun\.edu\.tr$").WithMessage("Email adresi @samsun.edu.tr domaini ile bitmelidir.");

            //RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre kısmı boş geçilemez.");
            //RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük harf içermelidir.");
            //RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Şifre en az bir küçük harf içermelidir.");
            //RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Şifre en az bir rakam içermelidir.");
            //RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifre uzunluğu en az 8 karakter olmalıdır.");
        }
    }
}

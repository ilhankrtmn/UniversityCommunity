﻿using FluentValidation;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Business.ValidationRules
{
    public class CommunityValidator : AbstractValidator<Community>
    {
        public CommunityValidator()
        {
            RuleFor(p => p.Email)
                    .NotEmpty().WithMessage("Şifre kısmı boş geçilemez.")
                    .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                    .Matches(@"@samsun\.edu\.tr$").WithMessage("Email adresi @samsun.edu.tr domaini ile bitmelidir.");
        }
    }
}

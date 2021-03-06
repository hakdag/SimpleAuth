﻿using FluentValidation;
using SimpleAuth.Api.Models.PasswordReset;

namespace SimpleAuth.Api.Validators.PasswordReset
{
    public class ValidatePasswordResetKeyValidator : AbstractValidator<ValidatePasswordResetKeyVM>
    {
        public ValidatePasswordResetKeyValidator()
        {
            RuleFor(m => m.UserName)
                .NotNull()
                .NotEmpty();
            RuleFor(m => m.ResetKey)
                .NotNull()
                .NotEmpty();
        }
    }
}

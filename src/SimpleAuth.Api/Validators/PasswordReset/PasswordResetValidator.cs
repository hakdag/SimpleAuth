using FluentValidation;
using SimpleAuth.Api.Models.PasswordReset;
using System;

namespace SimpleAuth.Api.Validators.PasswordReset
{
    public class PasswordResetValidator : AbstractValidator<PasswordResetVM>
    {
        public PasswordResetValidator()
        {
            RuleFor(m => m.UserName)
                .NotNull()
                .NotEmpty();
            RuleFor(m => m.ResetKey)
                .NotNull()
                .NotEmpty();
            RuleFor(m => m.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(100);
            RuleFor(m => m.PasswordConfirm)
                .Equal(m => m.Password).WithMessage("'Password Confirm' must be equal to Password.")
                .When(m => !String.IsNullOrWhiteSpace(m.Password));
        }
    }
}

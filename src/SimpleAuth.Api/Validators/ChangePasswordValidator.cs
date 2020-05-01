using FluentValidation;
using SimpleAuth.Api.Models;
using System;

namespace SimpleAuth.Api.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordValidator()
        {
            RuleFor(m => m.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(m => m.OldPassword)
                .NotNull()
                .NotEmpty()
                .NotEqual(m => m.Password).WithMessage("Old Password and Password can not be equal.")
                .When(m => !String.IsNullOrWhiteSpace(m.Password));
            RuleFor(m => m.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(100);
            RuleFor(m => m.PasswordConfirm)
                .Equal(m => m.Password).WithMessage("Password and Password Confirm must be equal.")
                .When(m => !String.IsNullOrWhiteSpace(m.Password));
        }
    }
}

using FluentValidation;
using SimpleAuth.Api.Models;
using System;

namespace SimpleAuth.Api.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserVM>
    {
        public CreateUserValidator()
        {
            RuleFor(m => m.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(m => m.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(100);
            RuleFor(m => m.PasswordConfirm)
                .Equal(m => m.Password)
                .When(m => !String.IsNullOrWhiteSpace(m.Password));
        }
    }
}

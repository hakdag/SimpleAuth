using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
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

using FluentValidation;
using SimpleAuth.Api.Models.PasswordReset;

namespace SimpleAuth.Api.Validators.PasswordReset
{
    public class GeneratePasswordResetKeyValidator : AbstractValidator<GeneratePasswordResetKeyVM>
    {
        public GeneratePasswordResetKeyValidator()
        {
            RuleFor(m => m.UserName)
                .NotNull()
                .NotEmpty();
        }
    }
}

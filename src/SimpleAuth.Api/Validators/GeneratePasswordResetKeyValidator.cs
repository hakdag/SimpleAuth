using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
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

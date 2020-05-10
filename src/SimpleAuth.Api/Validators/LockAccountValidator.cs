using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class LockAccountValidator : AbstractValidator<LockAccountVM>
    {
        public LockAccountValidator()
        {
            RuleFor(m => m.UserId)
                .GreaterThan(0);
        }
    }
}

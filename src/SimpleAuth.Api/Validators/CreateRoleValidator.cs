using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleVM>
    {
        public CreateRoleValidator()
        {
            RuleFor(m => m.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);
        }
    }
}

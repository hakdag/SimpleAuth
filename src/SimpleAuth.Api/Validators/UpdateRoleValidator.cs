using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleVM>
    {
        public UpdateRoleValidator()
        {
            RuleFor(m => m.NewRoleName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(m => m.RoleId)
                .GreaterThan(0);
        }
    }
}

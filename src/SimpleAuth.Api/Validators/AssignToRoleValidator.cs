using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class AssignToRoleValidator : AbstractValidator<AssignToRoleVM>
    {
        public AssignToRoleValidator()
        {
            RuleFor(m => m.UserId)
                .GreaterThan(0);
            RuleFor(m => m.RoleId)
                .GreaterThan(0);
        }
    }
}

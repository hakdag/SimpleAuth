using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class RolePermissionValidator : AbstractValidator<RolePermissionVM>
    {
        public RolePermissionValidator()
        {
            RuleFor(m => m.RoleId)
                .GreaterThan(0);
            RuleFor(m => m.PermissionId)
                .GreaterThan(0);
        }
    }
}

using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class UserPermissionValidator : AbstractValidator<UserPermissionVM>
    {
        public UserPermissionValidator()
        {
            RuleFor(m => m.UserId)
                .GreaterThan(0);
            RuleFor(m => m.PermissionId)
                .GreaterThan(0);
        }
    }
}

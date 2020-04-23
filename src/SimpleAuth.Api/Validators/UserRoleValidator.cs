using FluentValidation;
using SimpleAuth.Api.Models;

namespace SimpleAuth.Api.Validators
{
    public class UserRoleValidator : AbstractValidator<UserRoleVM>
    {
        public UserRoleValidator()
        {
            RuleFor(m => m.UserId)
                .GreaterThan(0);
            RuleFor(m => m.RoleId)
                .GreaterThan(0);
        }
    }
}

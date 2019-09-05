using FluentValidation;

namespace Saleforce.Permissions.Api.Core.Commands
{
    public class AssignRoleToUserValidator : AbstractValidator<AssignRoleToUser>
    {
        public AssignRoleToUserValidator()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty();
            RuleFor(cmd => cmd.Connector).NotEmpty();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Optional;
using Saleforce.Common;
using Saleforce.Common.Cqrs.Business;
using Saleforce.Common.Cqrs.Core;
using Saleforce.Common.EventSourcing.Core;
using Saleforce.Permissions.Api.Core.Commands;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Core.Services;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Business.CommandHandlers
{
    public class AssignRoleToUserHandler : TypedBaseHandler<AssignRoleToUser, RoleView>
    {
        private readonly IRoleAssignmentService _roleAssignmentService;
        public AssignRoleToUserHandler(
            IEventBus eventBus,
            ICommandValidator<AssignRoleToUser> validator,
            IRoleAssignmentService roleAssignmentService)
            : base(eventBus, validator)
        {
            _roleAssignmentService = roleAssignmentService;
        }

        public override async Task<Option<RoleView, Error>> ValidHandle(AssignRoleToUser command, CancellationToken cancellationToken)
        {
            var roleAssignmentExists = await _roleAssignmentService.RoleAssignmentExistsAsync(command);
            if (roleAssignmentExists)
            {
                Option.None<RoleView, Error>(Error.Conflict("Role assignment already exists!"));
            }

            return new Option<RoleView, Error>();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Saleforce.Permissions.Api.Core.Commands;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Core.Services;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Views;

using static Saleforce.Permissions.Api.Core.RoleTypes;

namespace Saleforce.Permissions.Api.Business.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly IRoleAssignmentRepository _roleAssignmentRepository;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IConfigurationService _configurationService;

        public RoleAssignmentService(
            IMapper mapper, 
            IRoleAssignmentRepository roleAssignmentRepository, 
            IPermissionsRepository permissionsRepository,
            IConfigurationService configurationService)
        {
            _mapper = mapper;
            _roleAssignmentRepository = roleAssignmentRepository;
            _permissionsRepository = permissionsRepository;
            _configurationService = configurationService;
        }

        public async Task<bool> RoleAssignmentExistsAsync(AssignRoleToUser model)
        {
            var userRoles = await _roleAssignmentRepository.GetRoleAssignmentsByUserAsync(model.UserId);

            var oldRoleAssignments = _mapper.Map<List<UserRoles>, List<RoleView>>(userRoles);

            var filteredRoleAssignments = oldRoleAssignments
                .Where(role => role.RoleType.Equals(model.RoleType, StringComparison.OrdinalIgnoreCase));

            return (from roleAssignment in filteredRoleAssignments
                let modelDataScopeDictionary = model.DataScope as IDictionary<string, JToken>
                let roleDataScopeDictionary = roleAssignment.DataScope as IDictionary<string, JToken>
                select AreRoleAssignmentsEqual(modelDataScopeDictionary, roleDataScopeDictionary)).FirstOrDefault();
        }

        public async Task<bool> CanUserAssignRoleAsync(
            string currentUserId,
            AssignRoleToUser command,
            CancellationToken cancellationToken)
        {
            var userPermissions = await _permissionsRepository.GetAssignRolePermissionsByUserIdAsync(currentUserId);
            var currentUserPermissions = _mapper.Map<List<UserPermissions>, List<UserPermissionView>>(userPermissions);

            var currentUserRoles = await _roleAssignmentRepository.GetRoleAssignmentsByUserAsync(currentUserId);
            if (currentUserRoles.Any(userRoles => userRoles.Role.RoleType.ToUpperInvariant().Equals(GlobalAdminRole.ToUpperInvariant())))
            {
                return true;
            }

            var roleConfigurations =
                await _configurationService.GetAssignableRoleConfigurationByUserIdAsync(currentUserId, cancellationToken);

            return false;
        }

        private static bool AreRoleAssignmentsEqual(
            IDictionary<string, JToken> modelDataScopeDictionary,
            IDictionary<string, JToken> roleAssignmentDataScopeDictionary)
        {
            if (modelDataScopeDictionary == null)
            {
                return roleAssignmentDataScopeDictionary == null;
            }

            if (roleAssignmentDataScopeDictionary == null)
            {
                return false;
            }

            if (ReferenceEquals(modelDataScopeDictionary, roleAssignmentDataScopeDictionary))
            {
                return true;
            }

            if (modelDataScopeDictionary.Count != roleAssignmentDataScopeDictionary.Count)
            {
                return false;
            }

            return modelDataScopeDictionary.Keys.All(roleAssignmentDataScopeDictionary.ContainsKey) &&
                   modelDataScopeDictionary.Keys.All(
                       key => JToken.DeepEquals(modelDataScopeDictionary[key], roleAssignmentDataScopeDictionary[key]));
        }


    }
}
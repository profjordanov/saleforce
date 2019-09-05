using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Core.Services;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Business.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IRoleAssignmentRepository _roleAssignmentRepository;
        private readonly IRulesConfigurationService _rulesConfigurationService;

        public ConfigurationService(IRoleAssignmentRepository roleAssignmentRepository, IRulesConfigurationService rulesConfigurationService)
        {
            _roleAssignmentRepository = roleAssignmentRepository;
            _rulesConfigurationService = rulesConfigurationService;
        }

        public async Task<List<ConfigurationView>> GetAssignableRoleConfigurationByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userRoleAssignments = await _roleAssignmentRepository.GetRoleAssignmentsByUserAsync(userId);
            var userRoles = userRoleAssignments.Select(x => x.Role.RoleType).ToList();

            var configurationList = new List<ConfigurationView>();
            foreach (var userRole in userRoles)
            {
                var roleRules = await _rulesConfigurationService.GetRulesAsync(cancellationToken);

                var configurationItem = roleRules
                    .Roles
                    .FirstOrDefault(role =>
                    role.RoleType.Equals(userRole, StringComparison.OrdinalIgnoreCase) &&
                    role.Definition.CanAssignRoles != null &&
                    role.Definition.CanAssignRoles.Count > 0);

                if (configurationItem == null)
                {
                    return configurationList;
                }

                var assignableRoles = configurationItem.Definition.CanAssignRoles;

                foreach (var assignableRole in assignableRoles)
                {
                    var assignableRoleConfigurationItem = roleRules
                        .Roles
                        .FirstOrDefault(
                            role => role.RoleType.Equals(assignableRole, StringComparison.OrdinalIgnoreCase));

                    if (assignableRoleConfigurationItem == null)
                    {
                        continue;
                    }

                    configurationList.Add(new ConfigurationView
                    {
                        RoleType = assignableRoleConfigurationItem.RoleType,
                        DataScopes = assignableRoleConfigurationItem
                            .Definition
                            .DataScopes
                            .Select(ds => new DataScopeView
                            {
                                DataScopeInformation = ds.DataScopeInformation,
                                IsRequired = ds.IsRequired,
                                MapTo = ds.MapTo,
                                MultiSelect = ds.MultiSelect ?? false
                            }).ToList(),
                         IsInternal = assignableRoleConfigurationItem.Definition.IsInternal
                    });
                }
            }

            return configurationList;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Saleforce.Permissions.Api.Entities;

namespace Saleforce.Permissions.Api.Core.Repositories
{
    public interface IPermissionsRepository
    {
        Task<List<UserPermissions>> GetAssignRolePermissionsByUserIdAsync(string userId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Saleforce.Permissions.Api.Entities;

namespace Saleforce.Permissions.Api.Core.Repositories
{
    public interface IRoleAssignmentRepository
    {
        Task<List<UserRoles>> GetRoleAssignmentsByUserAsync(string userId);
    }
}
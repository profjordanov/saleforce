using System.Threading.Tasks;
using Saleforce.Permissions.Api.Core.Commands;

namespace Saleforce.Permissions.Api.Core.Services
{
    public interface IRoleAssignmentService
    {
        Task<bool> RoleAssignmentExistsAsync(AssignRoleToUser model);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Persistence.EntityFramework;

namespace Saleforce.Permissions.Api.Persistence.Repositories
{
    public class RoleAssignmentRepository : IRoleAssignmentRepository
    {
        private readonly PermissionsDbContext _context;

        public RoleAssignmentRepository(PermissionsDbContext context)
        {
            _context = context;
        }

        public Task<List<UserRoles>> GetRoleAssignmentsByUserAsync(string userId) =>
            _context
                .UserRoles
                .Include(roles => roles.Role)
                .Where(roles => roles.UserId.ToUpperInvariant().Equals(userId.ToUpperInvariant()))
                .ToListAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Persistence.EntityFramework;

using static Saleforce.Permissions.Api.Core.PermissionTypes;

namespace Saleforce.Permissions.Api.Persistence.Repositories
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly PermissionsDbContext _dbContext;

        public PermissionsRepository(PermissionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<UserPermissions>> GetAssignRolePermissionsByUserIdAsync(string userId) =>
            _dbContext
                .UserPermissions
                .Include(permissions => permissions.Permission)
                .Where(permissions => permissions.UserId.ToUpperInvariant().Equals(userId.ToUpperInvariant()) &&
                                      (permissions.ExpireDate == null ||
                                       permissions.ExpireDate.HasValue &&
                                       permissions.ExpireDate.Value.Date >= DateTime.Now.Date))
                .Where(up => up.Permission.PermissionType.Equals(InviteUsersPermissionType, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
    }
}
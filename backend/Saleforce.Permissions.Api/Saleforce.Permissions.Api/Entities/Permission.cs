using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saleforce.Permissions.Api.Entities
{
    public class Permission
    {
        [Key]
        [Required]
        public string PermissionType { get; set; }

        public string Description { get; set; }

        public List<RolePermissions> RolePermissions { get; set; }
    }
}
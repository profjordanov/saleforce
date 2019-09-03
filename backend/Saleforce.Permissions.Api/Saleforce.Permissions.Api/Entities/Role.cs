using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saleforce.Permissions.Api.Entities
{
    public class Role
    {
        [Key]
        [Required]
        public string RoleType { get; set; }

        public string Description { get; set; }

        public List<RolePermissions> RolePermissions { get; set; }
    }
}
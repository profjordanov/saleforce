using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saleforce.Permissions.Api.Entities
{
    public class UserPermissions
    {
        public int Id { get; set; }

        [Required]
        public string Connector { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ExpireDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string DataScope { get; set; }

        [Required]
        [ForeignKey("PermissionType")]
        public Permission Permission { get; set; }

        [Required]
        public UserRoles UserRoles { get; set; }

        public static UserPermissions Create(string connector, string createdBy, DateTimeOffset? expireDate, string userId, string dataScope, Permission permission, UserRoles userRoles)
        {
            return new UserPermissions
            {
                Connector = connector,
                CreatedBy = createdBy,
                ExpireDate = expireDate,
                UserId = userId,
                DataScope = dataScope,
                Permission = permission,
                CreatedAt = DateTimeOffset.Now,
                UserRoles = userRoles
            };
        }
    }
}
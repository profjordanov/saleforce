using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Saleforce.Permissions.Api.Entities
{
    public class UserRoles
    {
        public int Id { get; private set; }

        public string Connector { get; private set; }

        public DateTimeOffset? ExpireDate { get; private set; }

        [Required]
        public string UserId { get; private set; }

        [Required]
        public string DataScope { get; private set; }

        [Required]
        public Role Role { get; private set; }

        public static UserRoles Create(string connector, string userId, string dataScope, Role role, DateTimeOffset? expireDate)
        {
            if (string.IsNullOrEmpty(connector))
            {
                throw new ArgumentNullException(nameof(Connector));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(UserId));
            }

            if (string.IsNullOrEmpty(dataScope))
            {
                throw new ArgumentNullException(nameof(DataScope));
            }

            if (role == null)
            {
                throw new ArgumentNullException(nameof(Role));
            }

            return new UserRoles
            {
                Connector = connector,
                DataScope = dataScope,
                Role = role,
                UserId = userId,
                ExpireDate = expireDate
            };
        }

        public UserRoles Update(string connector, DateTimeOffset? expireDate, string dataScope, Role role)
        {
            Connector = connector;
            ExpireDate = expireDate;
            DataScope = dataScope;
            if (Role.RoleType != role.RoleType)
            {
                Role = role;
            }
            return this;
        }
    }
}
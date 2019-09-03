namespace Saleforce.Permissions.Api.Entities
{
    public class RolePermissions
    {
        public string RoleType { get; set; }
        public Role Role { get; set; }

        public string PermissionType { get; set; }
        public Permission Permission { get; set; }
    }
}
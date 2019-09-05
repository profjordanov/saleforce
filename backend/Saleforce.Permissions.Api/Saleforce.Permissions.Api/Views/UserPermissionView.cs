namespace Saleforce.Permissions.Api.Views
{
    public class UserPermissionView
    {
        public string UserId { get; set; }

        public string Connector { get; set; }

        public string PermissionType { get; set; }

        public string DataScope { get; set; }

        public string ExpiryDate { get; set; }
    }
}
namespace Saleforce.Common.Hateoas
{
    public static class LinkNames
    {
        public const string Self = "self";

        public static class Auth
        {
            public const string Login = "login";
            public const string Register = "register";
            public const string GetCurrentUser = "get-current-user";
            public const string Logout = "logout";
        }

        public static class Customers
        {
            public const string GetAll = "get-all-customers";
            public const string RegisterCustomer = "register-customer";
            public const string UpdateRegisteredCustomer = "update-registered-customer";
            public const string DeleteCustomer = "delete-customer";
        }

        public static class Permissions
        {
            public const string GetRoleByUserId = "get-role-by-user";
            public const string GetRoleById = "get-role-by-id";
            public const string AssignRoleToUser = "assign-role-to-user";
            public const string UpdateRoleAssignment = "update-role-assignment";
            public const string DeleteRoleAssignment = "delete-role";
        }
    }
}
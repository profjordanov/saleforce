using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class PermissionGenerationRuleView
    {
        [JsonProperty("roleDataScope")]
        public List<RoleDataScopeView> RoleDataScope { get; set; }

        [JsonProperty("permissions")]
        public List<PermissionRoleRuleView> Permissions { get; set; }
    }
}
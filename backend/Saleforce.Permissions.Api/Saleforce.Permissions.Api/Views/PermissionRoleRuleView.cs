using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class PermissionRoleRuleView
    {
        [JsonProperty("permissionType")]
        public string PermissionType { get; set; }

        [JsonProperty("dataScope")]
        public List<string> DataScope { get; set; }
    }
}
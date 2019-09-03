using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class DefinitionView
    {
        [JsonProperty("dataScopes")]
        public List<DataScopeView> DataScopes { get; set; }

        [JsonProperty("permissionGenerationRules")]
        public List<PermissionGenerationRuleView> PermissionGenerationRules { get; set; }

        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("canAssignRoles")]
        public List<string> CanAssignRoles { get; set; }
    }
}
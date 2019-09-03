using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class RoleRuleView
    {
        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonProperty("definition")]
        public DefinitionView Definition { get; set; }
    }
}
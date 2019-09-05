using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class RoleDataScopeView
    {
        [JsonProperty("dataScope")]
        public string DataScope { get; set; }

        [JsonProperty("multiSelect")]
        public bool? MultiSelect { get; set; }
    }
}
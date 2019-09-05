using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class RoleView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("dataScope")]
        public dynamic DataScope { get; set; }

        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("connector")]
        public string Connector { get; set; }
    }
}
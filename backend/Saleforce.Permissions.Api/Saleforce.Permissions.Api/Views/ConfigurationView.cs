using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class ConfigurationView
    {
        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonProperty("dataScopes")]
        public List<DataScopeView> DataScopes { get; set; }

        public bool IsInternal { get; set; }
    }
}
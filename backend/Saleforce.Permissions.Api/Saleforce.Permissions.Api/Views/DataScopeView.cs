using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Views
{
    public class DataScopeView
    {
        [JsonProperty("dataScope")]
        public string DataScopeInformation { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }

        [JsonProperty("mapTo")]
        public string MapTo { get; set; }

        [JsonProperty("multiSelect")]
        public bool? MultiSelect { get; set; }
    }
}
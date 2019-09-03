using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Saleforce.Common.Cqrs.Core;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Core.Commands
{
    public class AssignRoleToUser : ICommand<RoleView>
    {
        public int Id { get; set; }

        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [Required]
        [JsonProperty("dataScope")]
        public dynamic DataScope { get; set; }

        [Required]
        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("connector")]
        public string Connector { get; set; }
    }
}
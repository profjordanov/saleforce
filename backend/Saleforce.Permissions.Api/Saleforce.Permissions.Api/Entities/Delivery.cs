using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Saleforce.Permissions.Api.Entities
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Data { get; set; }

        public string DeliveryApprovalId { get; set; }

        public DeliveryApproval DeliveryApproval { get; set; }

        [NotMapped]
        public DeliveryDomainModel DomainModel =>
            JsonConvert.DeserializeObject<DeliveryDomainModel>(Data);
    }
}
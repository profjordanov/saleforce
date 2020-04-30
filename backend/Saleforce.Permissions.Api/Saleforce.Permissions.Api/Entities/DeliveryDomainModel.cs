using System;

namespace Saleforce.Permissions.Api.Entities
{
    public class DeliveryDomainModel
    {
        public string DeliveryId { get; set; }

        public DateTimeOffset LoadDate { get; set; }
    }
}
using System.Collections.Generic;

namespace Saleforce.Permissions.Api.Entities
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<DeliveryApproval> DeliveryApprovals { get; set; }
    }
}
namespace Saleforce.Permissions.Api.Entities
{
    public class DeliveryApproval
    {
        public string Id { get; set; }

        public DeliveryStatus Status { get; set; }

        public Delivery Delivery { get; set; }

        public string UserInfoId { get; set; }

        public UserInfo UserInfo { get; set; }
    }

    public enum DeliveryStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 4
    }
}
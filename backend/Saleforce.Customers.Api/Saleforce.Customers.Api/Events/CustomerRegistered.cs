using System;
using Saleforce.Common.EventSourcing.Core;

namespace Saleforce.Customers.Api.Events
{
    public class CustomerRegistered : IEvent
    {
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}
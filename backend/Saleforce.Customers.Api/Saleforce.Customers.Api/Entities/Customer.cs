using System;
using Saleforce.Common.EventSourcing.Core;

namespace Saleforce.Customers.Api.Entities
{
    /// <summary>
    ///  Abstract data model that defines a data structure.
    /// </summary>
    public class Customer : IAggregate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
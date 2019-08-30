using System;

namespace Saleforce.Common.EventSourcing.Core
{
    public interface IAggregate
    {
        Guid Id { get; set; }
    }
}
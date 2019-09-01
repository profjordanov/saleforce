using System;
using RiskFirst.Hateoas;

namespace Saleforce.Common.Hateoas.Core
{
    public interface IPolicy<TResource>
        where TResource : Resource
    {
        Action<LinksPolicyBuilder<TResource>> PolicyConfiguration { get; }
    }
}
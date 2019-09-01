using System;
using RiskFirst.Hateoas;
using Saleforce.Common.Hateoas;
using Saleforce.Common.Hateoas.Core;

namespace Saleforce.Customers.Api.Hateoas.RegisterCustomerResources
{
    public class RegisterCustomerResourcePolicy : IPolicy<RegisterCustomerResource>
    {
        public Action<LinksPolicyBuilder<RegisterCustomerResource>> PolicyConfiguration => builder =>
        {
            builder.RequireSelfLink();
            //builder.RequireRoutedLink(LinkNames.Customers.GetAll, nameof())
        };
    }
}
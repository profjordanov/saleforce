using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Customers.Api.Core.Commands
{
    public class RegisterNewCustomer : ICommand
    {
        public string Name { get; set; }
    }
}
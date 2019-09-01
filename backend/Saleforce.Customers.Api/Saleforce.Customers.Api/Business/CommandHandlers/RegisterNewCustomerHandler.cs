using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Optional;
using Saleforce.Common;
using Saleforce.Common.Cqrs.Business;
using Saleforce.Common.Cqrs.Core;
using Saleforce.Common.EventSourcing.Core;
using Saleforce.Customers.Api.Core.Commands;

namespace Saleforce.Customers.Api.Business.CommandHandlers
{
    public class RegisterNewCustomerHandler : VoidBaseHandler<RegisterNewCustomer>
    {
        public RegisterNewCustomerHandler(
            IEventBus eventBus,
            ICommandValidator<RegisterNewCustomer> commandValidator)
            : base(eventBus, commandValidator)
        {
        }

        public override Task<Option<Unit, Error>> ValidHandle(
            RegisterNewCustomer command,
            CancellationToken cancellationToken) =>
            Task.FromResult(Option.Some<Unit, Error>(Unit.Value));
    }
}
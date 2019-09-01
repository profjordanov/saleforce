using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Optional;
using Optional.Async;
using Saleforce.Common.Cqrs.Core;
using Saleforce.Common.EventSourcing.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class VoidBaseHandler<TCommand> : BaseHandler, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected VoidBaseHandler(
            IEventBus eventBus,
            ICommandValidator<TCommand> commandValidator)
            : base(eventBus)
        {
            CommandValidator = commandValidator;
        }

        protected ICommandValidator<TCommand> CommandValidator { get; }

        public Task<Option<Unit, Error>> Handle(TCommand command, CancellationToken cancellationToken) =>
            CommandValidator.Validate(command)
                .FlatMapAsync(validCmd => ValidHandle(validCmd, cancellationToken));

        public abstract Task<Option<Unit, Error>> ValidHandle(
            TCommand command, CancellationToken cancellationToken);
    }
}
using Optional;
using Optional.Async;
using System.Threading;
using System.Threading.Tasks;
using Saleforce.Common.Cqrs.Core;
using Saleforce.Common.EventSourcing.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class TypedBaseHandler<TCommand, TResult> : BaseHandler, ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected TypedBaseHandler(
            IEventBus eventBus,
            ICommandValidator<TCommand> validator)
            : base(eventBus)
        {
            CommandValidator = validator;
        }

        protected ICommandValidator<TCommand> CommandValidator { get; }

        public Task<Option<TResult, Error>> Handle(TCommand request, CancellationToken cancellationToken) =>
            CommandValidator.Validate(request)
                .FlatMapAsync(command =>
                    ValidHandle(command, cancellationToken));

        public abstract Task<Option<TResult, Error>> ValidHandle(
            TCommand command,
            CancellationToken cancellationToken);
    }
}
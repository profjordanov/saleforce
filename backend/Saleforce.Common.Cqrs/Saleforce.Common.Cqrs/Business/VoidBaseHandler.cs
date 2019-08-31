using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Optional;
using Optional.Async;
using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class VoidBaseHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected VoidBaseHandler(ICommandValidator<TCommand> validator)
        {
            CommandValidator = validator;
        }

        protected ICommandValidator<TCommand> CommandValidator { get; }

        public Task<Option<Unit, Error>> Handle(TCommand command, CancellationToken cancellationToken) =>
            CommandValidator.Validate(command)
                .FlatMapAsync(validCmd => ValidHandle(validCmd, cancellationToken));

        public abstract Task<Option<Unit, Error>> ValidHandle(
            TCommand command, CancellationToken cancellationToken);
    }
}
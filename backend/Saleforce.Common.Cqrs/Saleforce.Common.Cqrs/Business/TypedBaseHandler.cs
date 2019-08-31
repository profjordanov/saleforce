using FluentValidation;
using Optional;
using Optional.Async;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class TypedBaseHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected TypedBaseHandler(ICommandValidator<TCommand> validator)
        {
            CommandValidator = validator;
        }

        protected ICommandValidator<TCommand> CommandValidator { get; }

        public Task<Option<TResult, Error>> Handle(TCommand request, CancellationToken cancellationToken) =>
            CommandValidator.Validate(request)
                .FlatMapAsync(command => ValidHandle(command, cancellationToken));

        public abstract Task<Option<TResult, Error>> ValidHandle(
            TCommand command, CancellationToken cancellationToken);
    }
}
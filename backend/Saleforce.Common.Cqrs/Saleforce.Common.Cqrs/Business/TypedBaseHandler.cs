using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Optional;
using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class TypedBaseHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected TypedBaseHandler(IValidator<TCommand> validator)
        {
            Validator = validator ??
                        throw new InvalidOperationException(
                            "Tried to instantiate a command handler without a validator." +
                            "Did you forget to add one?");
        }

        protected IValidator<TCommand> Validator { get; }

        public Task<Option<TResult, Error>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
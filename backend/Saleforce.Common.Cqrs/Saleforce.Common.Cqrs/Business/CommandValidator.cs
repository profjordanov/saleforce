using System;
using System.Linq;
using FluentValidation;
using Optional;
using Optional.Async;
using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public class CommandValidator<TCommand> : ICommandValidator<TCommand>
    {
        private readonly IValidator<TCommand> _validator;

        public CommandValidator(IValidator<TCommand> validator)
        {
            _validator = validator ??
                        throw new InvalidOperationException(
                            "Tried to instantiate a command handler without a validator." +
                            "Did you forget to add one?");
        }

        public Option<TCommand, Error> Validate(TCommand command)
        {
            var validationResult = _validator.Validate(command);

            return validationResult
                .SomeWhen(
                    r => r.IsValid,
                    r => Error.Validation(r.Errors.Select(e => e.ErrorMessage)))
                // If the validation result is successful, disregard it and simply return the command
                .Map(_ => command);
        }
    }
}
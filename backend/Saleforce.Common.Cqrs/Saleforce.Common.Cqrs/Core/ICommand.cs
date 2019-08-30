using MediatR;
using Optional;

namespace Saleforce.Common.Cqrs.Core
{
    public interface ICommand : IRequest<Option<Unit, Error>>
    {
    }

    public interface ICommand<TResult> : IRequest<Option<TResult, Error>>
    {
    }
}
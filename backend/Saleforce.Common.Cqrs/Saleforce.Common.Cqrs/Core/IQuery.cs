using MediatR;

namespace Saleforce.Common.Cqrs.Core
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
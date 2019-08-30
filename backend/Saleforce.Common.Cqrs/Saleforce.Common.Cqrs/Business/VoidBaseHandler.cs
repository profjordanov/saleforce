using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Optional;
using Saleforce.Common.Cqrs.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class VoidBaseHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public Task<Option<Unit, Error>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
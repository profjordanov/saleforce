using System;
using System.Threading.Tasks;
using MediatR;
using Saleforce.Common.EventSourcing.Core;

namespace Saleforce.Common.Cqrs.Business
{
    public abstract class BaseHandler
    {
        protected BaseHandler(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        protected Task<Unit> PublishEvents(Guid streamId, params IEvent[] events) =>
            EventBus.PublishAsync(streamId, events);
    }
}
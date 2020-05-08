using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackMachineApp.Infrastructure
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IEnumerable<IDomainEventHandler> handlers;

        public DomainEventDispatcher(IEnumerable<IDomainEventHandler> handlers)
        {
            this.handlers = handlers;
        }

        public Task Dispatch(IDomainEvent domainEvent)
        {
            foreach (var handler in handlers)
            {
                bool canHandleEvent = handler.GetType().GetInterfaces()
                    .Any(x => x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    //TODO: remove dynamic
                    dynamic handler2 = handler;
                    handler2.Handle((dynamic)domainEvent);
                }
            }

            return Task.CompletedTask;
        }
    }
}

using SnackMachineApp.Domain.Seedwork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackMachineApp.Infrastructure
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        public Task Dispatch(IDomainEvent domainEvent)
        {
            var _handlers = ObjectFactory.Instance.Resolve<IEnumerable<IDomainEventHandler>>();
            foreach (var handler in _handlers)
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

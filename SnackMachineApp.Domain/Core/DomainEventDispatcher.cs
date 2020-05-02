using SnackMachineApp.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SnackMachineApp.Domain.Core
{
    internal class DomainEventDispatcher: IDomainEventDispatcher
    {
        private static List<Type> _handlers;

        static DomainEventDispatcher()
        {
            _handlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
                .ToList();
        }

        public Task Dispatch(IDomainEvent domainEvent)
        {
            foreach (Type handlerType in _handlers)
            {
                bool canHandleEvent = handlerType.GetInterfaces()
                    .Any(x => x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    //TODO: possible to get through DI
                    dynamic handler = Activator.CreateInstance(handlerType);
                    handler.Handle((dynamic)domainEvent);
                }
            }

            return Task.CompletedTask;
        }
    }
}

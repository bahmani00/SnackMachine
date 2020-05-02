using Autofac;
using System;

namespace SnackMachineApp.Domain.Utils
{
    //Should be independent of any IoC
    public interface IComponentLocator
    {
        object Resolve(Type type);

        T Resolve<T>();
    }

    public class ComponentLocator : IComponentLocator
    {
        private readonly ILifetimeScope _Container;

        public ComponentLocator(ILifetimeScope container)
        {
            _Container = container;
        }

        public object Resolve(Type type)
        {
            return _Container.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _Container.Resolve<T>();
        }
    }
}
using Autofac;

namespace SnackMachineApp.Logic.Utils
{
    public class ComponentLocator : IComponentLocator
    {
        private readonly ILifetimeScope _Container;

        public ComponentLocator(ILifetimeScope container)
        {
            _Container = container;
        }

        public T ResolveComponent<T>()
        {
            return _Container.Resolve<T>();
        }
    }

    public interface IComponentLocator
    {
        T ResolveComponent<T>();
    }
}
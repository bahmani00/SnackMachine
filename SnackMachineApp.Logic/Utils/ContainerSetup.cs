using Autofac;
using CleanArchitecture.Core;
using System;
using System.Reflection;
using SnackMachineApp.Logic.Core.Interfaces;

namespace SnackMachineApp.Logic.Utils
{
    public static class ContainerSetup
    {
        public static IContainer BaseAutofacInitialization(Action<ContainerBuilder> setupAction = null)
        {
            var builder = new ContainerBuilder();

            var coreAssembly = Assembly.GetAssembly(typeof(DatabasePopulator));
            var sharedKernelAssembly = Assembly.GetAssembly(typeof(IRepository<>));
            builder.RegisterAssemblyTypes(sharedKernelAssembly, coreAssembly).AsImplementedInterfaces();

            setupAction?.Invoke(builder);
            return builder.Build();
        }
    }
}

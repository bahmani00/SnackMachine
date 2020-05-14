using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Infrastructure.IoC
{
    public sealed class LightInjectContainerSetup
    {
        public static IServiceProvider Init(string connectionString, string dbORM)
        {
            //Note: The default behavior in LightInject is to treat all objects as transients.

            var serviceCollection = new ServiceCollection();
            // The Microsoft.Extensions.Logging package provides this one-liner to have logging services.
            serviceCollection.AddLogging();

            var options = ContainerOptions.Default.WithMicrosoftSettings();
            options.EnablePropertyInjection = true;
            var container = new ServiceContainer(options);
            
            RegisterDbConnectionProvider(container, connectionString);

            container.AddJsonFile("ioc_modules.json");

            if(dbORM == "efcore")
                container.RegisterFrom<EfRegistrationModule>();
            else
                container.RegisterFrom<NHibernateRegistrationModule>();
            
            return container.CreateServiceProvider(serviceCollection);
        }

        private static void RegisterDbConnectionProvider(IServiceRegistry serviceRegistry, string connectionString)
        {
            serviceRegistry.RegisterInstance(new CommandsConnectionProvider(connectionString));
            serviceRegistry.RegisterInstance(new QueriesConnectionProvider(connectionString));
        }
    }
}

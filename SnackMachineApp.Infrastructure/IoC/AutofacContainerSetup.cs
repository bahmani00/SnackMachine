using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Infrastructure.IoC
{
    public sealed class AutofacContainerSetup
    {
        public static IServiceProvider Init(string connectionString, string dbORM)
        {
            var serviceCollection = new ServiceCollection();

            // The Microsoft.Extensions.Logging package provides this one-liner to have logging services.
            serviceCollection.AddLogging();

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);

            RegisterDbConnectionProvider(builder, connectionString);

            IConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile("ioc_modules.json");

            var configModule = new ConfigurationModule(config.Build());

            builder.RegisterModule(configModule);

            if (dbORM == "efcore")
                builder.RegisterModule<EfRegistrationModule>();
            else
                builder.RegisterModule<NHibernateRegistrationModule>();

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        private static void RegisterDbConnectionProvider(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterInstance(new CommandsConnectionProvider(connectionString))
                .As<CommandsConnectionProvider>().SingleInstance();
            builder.RegisterInstance(new QueriesConnectionProvider(connectionString))
                .As<QueriesConnectionProvider>().SingleInstance();
        }
    }
}

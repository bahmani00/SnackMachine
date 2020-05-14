using Autofac;
using LightInject;
using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using SnackMachineApp.Infrastructure.Data.EntityFramework;
using SnackMachineApp.Infrastructure.Data.NHibernate;
using SnackMachineApp.Infrastructure.Repositories;
using SnackMachineApp.Interface.Data;
using SnackMachineApp.Interface.Data.EntityFramework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SnackMachineApp.Infrastructure.IoC
{
    public sealed class ContainerSetup
    {
        public static IServiceProvider Init(string ioCCantainer, string connectionString, string dbORM)
        {
            var args = new object[] { connectionString, dbORM };
            var cantainer = Type.GetType(ioCCantainer)
                .GetMethod("Init", BindingFlags.Public | BindingFlags.Static)
                .Invoke(null, args);

            return (IServiceProvider)cantainer;
        }
    }
    public class DomainEventDispatcherRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventDispatcher>().As<IDomainEventDispatcher>();
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }

    public class DapperRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnection(c.Resolve<QueriesConnectionProvider>().Value))
                .As<IDbConnection>();

            builder.RegisterType<DapperRepositor1y>();
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDbConnection>(c => new SqlConnection(c.GetInstance<QueriesConnectionProvider>().Value));

            serviceRegistry.Register<DapperRepositor1y>();
        }
    }

    public class NHibernateRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            //RegisterInstance method allows you to register an instance not built by Autofac.
            //https://stackoverflow.com/questions/31582000/autofac-registerinstance-vs-singleinstance
            builder.RegisterType<SessionFactory>().SingleInstance();
            builder.RegisterType<NHibernateUnitOfWork>().As<ITransactionUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(NHibernateDbPersister<>)).As(typeof(IDbPersister<>));
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterSingleton<SessionFactory>();
            serviceRegistry.Register<ITransactionUnitOfWork, NHibernateUnitOfWork>(new PerScopeLifetime());
            serviceRegistry.Register(typeof(IDbPersister<>), typeof(NHibernateDbPersister<>));
        }
    }

    public class EfRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            //RegisterInstance method allows you to register an instance not built by Autofac.
            //https://stackoverflow.com/questions/31582000/autofac-registerinstance-vs-singleinstance
            builder.RegisterType<EfUnitOfWork>().As<ITransactionUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<AppDbContext>().As<DbContext>();
            builder.RegisterGeneric(typeof(EFDbPersister<>)).As(typeof(IDbPersister<>));
        }
        
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ITransactionUnitOfWork, EfUnitOfWork>(new PerScopeLifetime());
            serviceRegistry.Register<DbContext, AppDbContext>();
            serviceRegistry.Register(typeof(IDbPersister<>), typeof(EFDbPersister<>));
        }
    }

    public class RepositoryRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Repository<>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + t.Name))
                .PropertiesAutowired();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).PropertiesAutowired();
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            //serviceRegistry.RegisterAssembly(typeof(IRepository<>).Assembly
            //    , (serviceType, implementingType) =>
            //        implementingType.Name.EndsWith("Repository") &&
            //        (serviceType.GetInterfaces()?.Any(i => i.Name == "I" + serviceType.Name)).GetValueOrDefault());

            serviceRegistry.Register(typeof(IHeadOfficeRepository), typeof(HeadOfficeRepository), new PerScopeLifetime());
            serviceRegistry.Register(typeof(IAtmRepository), typeof(AtmRepository), new PerScopeLifetime());
            serviceRegistry.Register(typeof(ISnackMachineRepository), typeof(SnackMachineRepository), new PerScopeLifetime());

            serviceRegistry.Register(typeof(IRepository<>), typeof(Repository<>), new PerScopeLifetime());
        }
    }

    public class DecoratorsRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterDecorator<ThreadScopedSnackMachineServiceDecorator, ISnackMachineService>();

            //builder.RegisterDecorator<ISnackMachineService>(
            //    (c, inner) => c.ResolveNamed<ISnackMachineService>("decorator", TypedParameter.From(inner)), "original")
            //    .As<ISnackMachineService>();

            //builder.RegisterType<ThreadScopedSnackMachineServiceDecorator>()
            //       .InstancePerLifetimeScope();

        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
        }
    }
}

using Autofac;
using AutoMapper;
using LightInject;
using SnackMachineApp.Application.Atms;
using SnackMachineApp.Application.Management;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Application.SnackMachines;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;
using System;
using System.Linq;

namespace SnackMachineApp.Application
{
    public class AutoMapperModule : Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtmDto, Atm>();
                cfg.CreateMap<SnackMachineDto, SnackMachine>();
                //cfg.CreateMap<HeadOfficeDto, HeadOffice>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
        }
    }
    public class DomainEventHandlersRegistrationModule : Autofac.Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            //When hosting applications in IIS all assemblies are loaded into the AppDomain when the application first starts, 
            //but when the AppDomain is recycled by IIS the assemblies are then only loaded on demand.
            //System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            //https://autofaccn.readthedocs.io/en/latest/register/scanning.html

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("DomainEventHandler") &&
                       t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
                   .As<IDomainEventHandler>();
                   //.As(t => t.GetInterfaces().First(i => i.Name == typeof(IDomainEventHandler<>).Name));
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(typeof(BalanceChangedDomainEventHandler).Assembly,
                (s, _) => s.IsGenericType && s.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));
        }
    }

    public class MediatorRegistrationModule : Module, ICompositionRoot
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().SingleInstance();
            builder.RegisterType<PaymentGateway>().As<IPaymentGateway>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(IRequestHandler<,>).Assembly)
               .Where(t => t.Name.EndsWith("Handler") &&
                       t.GetInterfaces().Any(y => y.IsGenericType &&
                       y.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
               .As(t => t.GetInterfaces().First(i => i.Name == typeof(IRequestHandler<,>).Name));
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterSingleton<IMediator, Mediator>();
            serviceRegistry.RegisterSingleton<IPaymentGateway,PaymentGateway>();

            serviceRegistry.RegisterAssembly(typeof(IRequestHandler<,>).Assembly,
                (s, _) => s.IsGenericType && s.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
        }
    }

}
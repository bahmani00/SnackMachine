using Autofac;
using Autofac.Builder;
using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Management;
using System;
using System.Linq;
using System.Reflection;

namespace SnackMachineApp.Logic.Utils
{
    public static class ContainerSetup
    {
        public static IContainer Container { get; private set; }

        public static IContainer Init(string connectionString)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var repositoriesAssembly = Assembly.GetAssembly(typeof(Repository<>));

            var builder = new ContainerBuilder();
            //RegisterInstance method allows you to register an instance not built by Autofac.
            //https://stackoverflow.com/questions/31582000/autofac-registerinstance-vs-singleinstance
            builder.RegisterInstance(new SessionFactory(connectionString)).As<SessionFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(NHibernateDbPersister<>)).As(typeof(IDbPersister<>)).InstancePerDependency();

            builder.RegisterType<ComponentLocator>().As<IComponentLocator>();

            builder.RegisterModule(new RepositoryRegistrationModule());

            builder.RegisterModule(new EventHandlersRegistrationModule());


            builder.Register((c, p) =>
                  {
                      var headOfficeId = p.Named<long>("HeadOfficeId");
                      if (headOfficeId == 1)
                      {
                          return c.Resolve<IRepository<HeadOffice>>().GetById(headOfficeId);
                      }
                      throw new ArgumentException("Invalid HeadOfficeId");
                  }).SingleInstance();


            builder.RegisterModule(new DecoratorsRegistrationModule());


            return (Container = builder.Build());
        }

        public static void DisplayRegistrations()
        {
            var registrations = Container.ComponentRegistry.Registrations;
            //.Where(r => typeof(IDbPersister<>).IsAssignableFrom(r.Activator.LimitType))
            //.Select(r => r.Activator.LimitType);

            foreach (var registration in registrations)
            {
                foreach (var service in registration.Services)
                    Console.Out.WriteLine(service.Description);
            }
        }

        static Type GetIRepositoryType(Type typeInstance, Type genericType)
        {
            return typeInstance.GetInterfaces()
                .Where(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == genericType)
                .Single().GetGenericTypeDefinition();
        }
    }

    public class RepositoryRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Repository<>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + t.Name))
                .PropertiesAutowired();
            //.WithParameter(new TypedParameter(typeof(string), "easyBlog"));

            //builder.RegisterAssemblyTypes(repositoriesAssembly)
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces().PropertiesAutowired();

            //working: builder.RegisterType<HeadOffice>().InjectFields(true);
            //builder.RegisterType(typeof(Repository<>)).InjectFields();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();//.InjectFields(true);


            //builder.RegisterAssemblyTypes(repositoriesAssembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IRepository<>)))
            //    .As(t => new Autofac.Core.KeyedService("repo2", GetIRepositoryType(t)))
            //    .PropertiesAutowired();

            //builder.RegisterAssemblyTypes(repositoriesAssembly)
            //    .AsClosedTypesOf(typeof(Repository<>))
            //    .OnActivated(args =>
            //    {
            //        var type = args.Instance.GetType();
            //        if (type.GetGenericTypeDefinition() == typeof(Repository<>))
            //        {
            //            type.GetField("dbPersister")
            //            .SetValue(args.Instance, args.Context.Resolve(type.GetInterfaces()[0]));
            //        }
            //    });
        }
    }

    public class EventHandlersRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IDomainEventHandler<>).Assembly)
                .Where(t => t.Name.EndsWith("EventHandler")).AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //    .AsClosedTypesOf(typeof(IDomainEventHandler<>));

        }
    }

    public class DecoratorsRegistrationModule : Autofac.Module
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
    }

    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> InjectFields
            (this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> builder)
        {
            builder.OnActivated(args => InjectFields(args.Context, args.Instance));

            return builder;
        }

        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> InjectFields<T>
            (this IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder)
        {
            builder.OnActivated(args => InjectFields(args.Context, args.Instance));

            return builder;
        }

        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> InjectProperties<T>
            (this IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder, bool overrideSetValues = true)
        {
            builder.OnActivated(args => InjectProperties(args.Context, args.Instance, overrideSetValues));

            return builder;
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> InjectProperties
              (this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> builder)
        {
            builder.OnActivated(args => InjectProperties(args.Context, args.Instance));

            return builder;
        }

        public static void InjectProperties(IComponentContext context,
               object instance, bool overrideSetValues = true)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (instance == null)
                throw new ArgumentNullException("instance");
            foreach (
               PropertyInfo propertyInfo in
                   //BindingFlags.NonPublic flag added for non public properties
                   instance.GetType().GetProperties(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic))
            {
                Type propertyType = propertyInfo.PropertyType;
                if ((!propertyType.IsValueType || propertyType.IsEnum) &&
                    (propertyInfo.GetIndexParameters().Length == 0 &&
                        context.IsRegistered(propertyType)))
                {
                    //Changed to GetAccessors(true) to return non public accessors
                    MethodInfo[] accessors = propertyInfo.GetAccessors(true);
                    if ((accessors.Length != 1 ||
                        !(accessors[0].ReturnType != typeof(void))) &&
                         (overrideSetValues || accessors.Length != 2 ||
                         propertyInfo.GetValue(instance, null) == null))
                    {
                        object obj = context.Resolve(propertyType);
                        propertyInfo.SetValue(instance, obj, null);
                    }
                }
            }
        }

        public static void InjectFields(IComponentContext context, object instance)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (instance == null)
                throw new ArgumentNullException("instance");

            foreach (var fieldInfo in instance.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Type fieldType = fieldInfo.FieldType;
                if (//(!fieldType.IsValueType || fieldType.IsEnum) && 
                    context.IsRegistered(fieldType))
                {
                    object obj = context.Resolve(fieldType);

                    fieldInfo.SetValue(instance, obj);
                }
            }
        }
    }

}

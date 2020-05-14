using Autofac;
using Autofac.Builder;
using System;
using System.Reflection;

namespace SnackMachineApp.Infrastructure.IoC
{
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
                    propertyInfo.GetIndexParameters().Length == 0 &&
                        context.IsRegistered(propertyType))
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

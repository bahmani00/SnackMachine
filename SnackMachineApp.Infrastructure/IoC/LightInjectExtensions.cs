using LightInject;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SnackMachineApp.Infrastructure.IoC
{
    //Disclamer: https://github.com/dotnetcore/AspectCore-Framework/blob/1640a332c707d6583281d25989d02940ce981cc9/src/AspectCore.Extensions.LightInject/LightInjectExtensions.cs
    internal static class LightInjectExtensions
    {
        public static IServiceRegistry RegisterSingleton<T, TImpl>(this IServiceRegistry services, string name = default)
            where T : class
            where TImpl : class, T
        {
            return services.Register<T, TImpl>(name ?? string.Empty, new PerContainerLifetime());
        }

        public static IServiceRegistry RegisterSingleton<T>(this IServiceRegistry services, T instance)
            where T : class
        {
            return services.RegisterInstance(instance);
        }

        public static void AddJsonFile(this IServiceRegistry services, string path)
        {
            var method = services.GetType().GetMethods()
                .FirstOrDefault(x => x.IsGenericMethod && x.IsPublic && !x.IsStatic && x.Name == nameof(services.RegisterFrom));

            var rootObject = JsonSerializer.Deserialize<Rootobject>(File.ReadAllText(path));
            foreach (var module in rootObject.modules)
            {
                method.MakeGenericMethod(Type.GetType(module.type)).Invoke(services, null);
            }
        }
    }


    public class Rootobject
    {
        public Module[] modules { get; set; }
    }

    public class Module
    {
        public string type { get; set; }
    }
}
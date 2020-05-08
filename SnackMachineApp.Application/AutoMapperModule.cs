using Autofac;
using AutoMapper;
using SnackMachineApp.Application.Atms;
using SnackMachineApp.Application.SnackMachines;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application
{
    public class AutoMapperModule : Module
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
    }
}
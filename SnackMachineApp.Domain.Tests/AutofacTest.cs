using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Management;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using System;
using Xunit;

namespace SnackMachineApp.Domain.Tests
{
    public class AutofacTest
    {
        private IServiceProvider serviceProvider;

        public AutofacTest()
        {
            Infrastructure.ObjectFactory.Instance.GetType();
            serviceProvider = Infrastructure.ObjectFactory.Instance.GetService<IServiceProvider>();
        }

        [Fact]
        public void ContainerSetup_BalanceChangedEventHandler_Works()
        {
            //Arranage
            var handler = serviceProvider.GetService<BalanceChangedDomainEventHandler>();

            //Act
            Action action = () => handler.Handle(null);

            //Assert
            handler.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ContainerSetup_IDbPersister_Works()
        {
            //Arranage
            var dbPersister = Infrastructure.ObjectFactory.Instance.GetService<IDbPersister<SnackMachine>>();

            //Act
            Func<SnackMachine> action = () => dbPersister.GetById(1);

            //Assert
            dbPersister.Should().NotBeNull();
            action().Should().NotBeNull();
        }

        [Fact]
        public void ContainerSetup_IDbPersister_Fields_are_set()
        {
            //Arranage
            var repository = Infrastructure.ObjectFactory.Instance.GetService<IAtmRepository>();

            //Act
            Func<Atm> action = () => repository.GetById(1);

            //Assert
            repository.Should().NotBeNull();
            action().Should().NotBeNull();
        }

        [Fact]
        public void ContainerSetup_Repository_Works()
        {
            //Arranage
            var repository = Infrastructure.ObjectFactory.Instance.GetService<IAtmRepository>();

            //Act
            Func<Atm> action = () => repository.GetById(1);

            //Assert
            repository.Should().NotBeNull();
            action().Should().NotBeNull();
        }

      

    }
}

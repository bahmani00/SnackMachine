using FluentAssertions;
using SnackMachineApp.Application.Management;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;
using SnackMachineApp.Infrastructure.Data;
using System;
using Xunit;

namespace SnackMachineApp.Domain.Tests
{
    public class AutofacTest: IDisposable
    {

        public AutofacTest()
        {
            ObjectFactory.Instance.GetType();//.Init(ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString);
        }
        public void Dispose()
        {
            ObjectFactory.Instance.Dispose();
        }


        [Fact]
        public void ContainerSetup_BalanceChangedEventHandler_Works()
        {
            //Arranage
            var handler = ObjectFactory.Instance.Resolve<BalanceChangedDomainEventHandler>();

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
            var dbPersister = ObjectFactory.Instance.Resolve<IDbPersister<SnackMachine>>();

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
            var repository = ObjectFactory.Instance.Resolve<IAtmRepository>();

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
            var repository = ObjectFactory.Instance.Resolve<IAtmRepository>();

            //Act
            Func<Atm> action = () => repository.GetById(1);

            //Assert
            repository.Should().NotBeNull();
            action().Should().NotBeNull();
        }

      

    }
}

using Autofac;
using FluentAssertions;
using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.Logic.Utils;
using System;
using System.Configuration;
using Xunit;

namespace SnackMachineApp.Logic.Tests
{
    public class AutofacTest: IDisposable
    {
        private IContainer _Container;

        public AutofacTest()
        {
            ContainerSetup.Init(ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString);
            _Container = ContainerSetup.Container;
        }
        public void Dispose()
        {
            _Container.Dispose();
        }


        [Fact]
        public void ContainerSetup_BalanceChangedEventHandler_Works()
        {
            //Arranage
            var handler = _Container.Resolve<Atms.BalanceChangedEventHandler>();

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
            var dbPersister = _Container.Resolve<IDbPersister<SnackMachine>>();

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
            var repository = _Container.Resolve<IAtmRepository>();

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
            var repository = _Container.Resolve<IAtmRepository>();

            //Act
            Func<Atm> action = () => repository.GetById(1);

            //Assert
            repository.Should().NotBeNull();
            action().Should().NotBeNull();
        }

      

    }
}

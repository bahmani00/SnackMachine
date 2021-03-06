﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Management;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using SnackMachineApp.Infrastructure.IoC;
using System;
using System.Configuration;
using Xunit;

namespace SnackMachineApp.Domain.Tests
{
    public class AutofacTest
    {
        private IServiceProvider serviceProvider;

        public AutofacTest()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString;
            var ioCContainer = ConfigurationManager.AppSettings["IoCContainer"];
            var dbORM = ConfigurationManager.AppSettings["ORM"];

            serviceProvider = ContainerSetup.Init(ioCContainer, connectionString, dbORM);
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
            var dbPersister = serviceProvider.GetService<IDbPersister<SnackMachine>>();

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
            var repository = serviceProvider.GetService<IAtmRepository>();

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
            var repository = serviceProvider.GetService<IAtmRepository>();

            //Act
            Func<Atm> action = () => repository.GetById(1);

            //Assert
            repository.Should().NotBeNull();
            action().Should().NotBeNull();
        }

      

    }
}

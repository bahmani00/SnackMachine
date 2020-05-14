using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.IoC;
using System;
using System.Configuration;
using Xunit;
using static SnackMachineApp.Domain.SharedKernel.Money;

namespace SnackMachineApp.Domain.Tests
{
    public class RepositoryTest
    {
        private IServiceProvider serviceProvider;

        public RepositoryTest()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString;
            var ioCContainer = ConfigurationManager.AppSettings["IoCContainer"];
            var dbORM = ConfigurationManager.AppSettings["ORM"];

            serviceProvider = ContainerSetup.Init(ioCContainer, connectionString, dbORM);
        }

        [Fact]
        public void Save_LoadSnackMachin_then_BuySnack_changes_are_Saved_in_database()
        {
            //Arranage
            var repository = serviceProvider.GetService<ISnackMachineRepository>();
            var snackMachine = repository.GetById(1);

            //Act
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);
            snackMachine.BuySnack(1);
            repository.Save(snackMachine);

            //Assert
            snackMachine = repository.GetById(1);
        }

    }
}

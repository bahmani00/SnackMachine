using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;
using System;
using Xunit;
using static SnackMachineApp.Domain.SharedKernel.Money;

namespace SnackMachineApp.Domain.Tests
{
    public class RepositoryTest : IDisposable
    {
        public RepositoryTest()
        {
            ObjectFactory.Instance.GetType();
        }
        public void Dispose()
        {
            ObjectFactory.Instance.Dispose();
        }

        [Fact]
        public void Save_LoadSnackMachin_then_BuySnack_changes_are_Saved_in_database()
        {
            //Arranage
            //Initer.Init(ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString);

            var repository = ObjectFactory.Instance.Resolve<ISnackMachineRepository>();
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

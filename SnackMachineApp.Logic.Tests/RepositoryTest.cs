using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.Logic.Utils;
using System;
using System.Configuration;
using Xunit;
using static SnackMachineApp.Logic.SharedKernel.Money;

namespace SnackMachineApp.Logic.Tests
{
    public class RepositoryTest : IDisposable
    {
        public RepositoryTest()
        {
            ObjectFactory.Instance.GetType();//.Init(ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString);
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

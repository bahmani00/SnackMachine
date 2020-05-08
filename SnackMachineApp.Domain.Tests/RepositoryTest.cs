using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Domain.SnackMachines;
using Xunit;
using static SnackMachineApp.Domain.SharedKernel.Money;

namespace SnackMachineApp.Domain.Tests
{
    public class RepositoryTest
    {
        public RepositoryTest()
        {
            Infrastructure.ObjectFactory.Instance.GetType();
        }

        [Fact]
        public void Save_LoadSnackMachin_then_BuySnack_changes_are_Saved_in_database()
        {
            //Arranage
            var repository = Infrastructure.ObjectFactory.Instance.GetService<ISnackMachineRepository>();
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

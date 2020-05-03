using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Infrastructure.Data
{
    public static class DatabasePopulator
    {
        public static void PopulateDatabase()
        {
            using (var context = ObjectFactory.Instance.Resolve<DbContext>())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //var snackMachine = new SnackMachine { Id = 1};
                //snackMachine.InsertMoney(Money.Cent * 100 + Money.TenCent * 100 + Money.Quarter * 100 + 
                //    Money.Dollar * 100 + Money.FiveDollar * 10 + Money.TwentyDollar * 10);

                //snackMachine.LoadSnacks(1, new SnackPile(new Snack(1, "Chocolate"), 20, 1m));
                //snackMachine.LoadSnacks(2, new SnackPile(new Snack(2, "Cookie"), 20, 2m));
                //snackMachine.LoadSnacks(3, new SnackPile(new Snack(3, "Gum"), 20, 3m));

                ////snackMachine.Slots[0].Id = 1;
                ////snackMachine.Slots[1].Id = 3;
                ////snackMachine.Slots[2].Id = 3;

                //context.Add(snackMachine);

                //var atm = new Atm() { Id = 1};
                //atm.LoadMoney(Money.Cent * 100 + Money.TenCent * 100 + Money.Quarter * 100 +
                //    Money.Dollar * 100 + Money.FiveDollar * 10 + Money.TwentyDollar * 10);
                //atm.Withdrawal(1.0m);
                //context.Add(atm);

                //var headOffice = new HeadOffice() { Id = 1 };
                //headOffice.ChangeBalance(100);
                //context.Add(headOffice);


                //context.SaveChanges();
            }
        }
    }
}

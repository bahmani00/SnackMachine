using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.Logic
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
            //InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(TenCent);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(FiveDollar);
            snackMachine.InsertMoney(TwentyDollar);
            for (var position = 0; position < 3; position++)
            {
                var snackPile = snackMachine.GetSnackPile(position);
                //snackPile.Price = position;

            }

        }
    }
}
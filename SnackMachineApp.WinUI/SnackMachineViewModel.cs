using SnackMachineApp.Logic;
using SnackMachine.UI.Common;
using System;

using static SnackMachineApp.Logic.Money;

namespace SnackMachine.UI
{
    public class SnackMachineViewModel : ViewModel
    {
        private SnackMachineApp.Logic.SnackMachine snackMachine;


        public override string Caption => "Snack Machine";

        public Command InsertCentCommand { get; private set; }
        public Command InsertTenCentCommand { get; private set; }
        public Command InsertQuarterCommand { get; private set; }
        public Command InsertDollarCommand { get; private set; }
        public Command InsertFiveDollarCommand { get; private set; }
        public Command InsertTwentyDollarCommand { get; private set; }
        public Command ReturnMoneyCommand { get; private set; }
        public Command BuySnackCommand { get; private set; }

        public string MoneyInTransaction => snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => snackMachine.MoneyInside + snackMachine.MoneyInTransaction;

        private string _message = "";
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                Notify();
            }
        }

        public SnackMachineViewModel(SnackMachineApp.Logic.SnackMachine snackMachine)
        {
            this.snackMachine = snackMachine;

            InsertCentCommand = new Command(() => InsertMoney(Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));

            ReturnMoneyCommand = new Command(() => ReturnMoney());
            BuySnackCommand = new Command(() => BuySnack());

        }

        private void BuySnack()
        {
            snackMachine.BuySnack();
            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(snackMachine);
                transaction.Commit();
            }

            NotifyClient("You have bought a snack");
        }

        private void ReturnMoney()
        {
            snackMachine.ReturnMoney();
            NotifyClient("Money was returned");
        }

        private void InsertMoney(Money coinOrNote)
        {
            snackMachine.InsertMoney(coinOrNote);
            NotifyClient("You have inserted: " + coinOrNote);
        }

        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInTransaction));
            Notify(nameof(MoneyInside));
        }

    }
}

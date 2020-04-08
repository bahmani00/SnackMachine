using SnackMachineApp.Logic;
using SnackMachineApp.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnackMachineApp.Logic.Money;

namespace SnackMachineApp.WinUI
{
    public class SnackMachineViewModel : ViewModel
    {
        private SnackMachine snackMachine;
        private readonly SnackMachineRepository repository;


        public override string Caption => "Snack Machine";

        public Command InsertCentCommand { get; private set; }
        public Command InsertTenCentCommand { get; private set; }
        public Command InsertQuarterCommand { get; private set; }
        public Command InsertDollarCommand { get; private set; }
        public Command InsertFiveDollarCommand { get; private set; }
        public Command InsertTwentyDollarCommand { get; private set; }
        public Command ReturnMoneyCommand { get; private set; }
        public Command<string> BuySnackCommand { get; private set; }

        public string MoneyInTransaction => snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => snackMachine.MoneyInside;

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

        public IReadOnlyList<SnackPileViewModel> Piles
        {
            get
            {
                return snackMachine.GetAllSnackPiles()
                    .Select(x => new SnackPileViewModel(x))
                    .ToList();
            }
        }
        public SnackMachineViewModel(SnackMachine snackMachine)
        {
            this.snackMachine = snackMachine;
            repository = new SnackMachineRepository();

            InsertCentCommand = new Command(() => InsertMoney(Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(TwentyDollar));

            ReturnMoneyCommand = new Command(() => ReturnMoney());
            BuySnackCommand = new Command<string>(BuySnack);
        }

        private void BuySnack(string position)
        {
            var pos = Convert.ToInt32(position);
            if (!snackMachine.CanBuySnack(pos))
            {
                Message = snackMachine.ValidationMessages.Project();
                return;
            }
            
            snackMachine.BuySnack(pos);
            repository.Save(snackMachine);

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
            Notify(nameof(Piles));
        }

    }
}

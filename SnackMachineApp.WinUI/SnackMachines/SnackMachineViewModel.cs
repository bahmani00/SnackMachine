using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Application.SnackMachines;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Domain.Utils;
using SnackMachineApp.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.WinUI.SnackMachines
{
    public class SnackMachineViewModel : ViewModel
    {
        private SnackMachine snackMachine;
        private readonly IMediator _mediator;

        public override string Caption => "Snack Machine";

        public Command<string> InsertCommand { get; private set; }

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

        public IReadOnlyList<SnackPileViewModel> Piles => 
            snackMachine
                .GetAllSnackPiles()
                .Select(x => new SnackPileViewModel(x))
                .ToList();

        public SnackMachineViewModel(IMediator mediator, SnackMachine snackMachine)
        {
            this._mediator = mediator;
            this.snackMachine = snackMachine;

            InsertCommand = new Command<string>(InsertMoney);

            ReturnMoneyCommand = new Command(() => ReturnMoney());
            BuySnackCommand = new Command<string>(BuySnack);
        }

        private void BuySnack(string position)
        {
            var pos = Convert.ToInt32(position);
            _mediator.Send(new BuySnackCommand(snackMachine, pos));

            if (snackMachine.AnyErrors())
            {
                Message = snackMachine.Project();
                return;
            }

            NotifyClient("You have bought a snack");
        }

        private void ReturnMoney()
        {
            _mediator.Send(new ReturnMoneyCommand(snackMachine));
            NotifyClient("Money was returned");
        }

        private void InsertMoney(string coinOrNote)
        {
            var money = Money.From(Convert.ToDecimal(coinOrNote));

            _mediator.Send(new InsertMoneyCommand(snackMachine, money));
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

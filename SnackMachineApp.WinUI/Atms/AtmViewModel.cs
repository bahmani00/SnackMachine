﻿using SnackMachineApp.Application.Atms;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.Utils;
using SnackMachineApp.WinUI.Common;

namespace SnackMachineApp.WinUI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private Atm _atm;
        private readonly IMediator _mediator;
        private string _message;
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                Notify();
            }
        }

        public override string Caption => "ATM";
        public Money MoneyInside => _atm.MoneyInside;
        public string MoneyCharged => _atm.MoneyCharged.ToString("C2");
        public Command<decimal> TakeMoneyCommand { get; private set; }

        public AtmViewModel(IMediator _mediator, Atm atm)
        {
            this._mediator = _mediator;
            this._atm = atm;
            
            TakeMoneyCommand = new Command<decimal>(x => x > 0, Withdraw);
        }

        private void Withdraw(decimal amount)
        {
            _atm = _mediator.Send(new WithdrawCommand(_atm.Id, amount));

            if (_atm.AnyErrors())
            {
                Message = _atm.Project();
                return;
            }

            NotifyClient("You have taken " + amount.ToString("C2"));
        }

        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInside));
            Notify(nameof(MoneyCharged));
        }
    }
}

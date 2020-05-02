using SnackMachineApp.Domain.Atms;
using SnackMachineApp.WinUI.Common;
using SnackMachineApp.Domain.Utils;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.Core.Interfaces;

namespace SnackMachineApp.WinUI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private readonly Atm _atm;
        private readonly IMediator mediator;
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

        public AtmViewModel(Atm atm)
        {
            _atm = atm;

            mediator = ObjectFactory.Instance.Resolve<IMediator>();

            TakeMoneyCommand = new Command<decimal>(x => x > 0, Withdrawal);
        }

        private void Withdrawal(decimal amount)
        {
            mediator.Send(new WithdrawCommand(_atm, amount));

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

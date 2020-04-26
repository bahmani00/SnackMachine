using SnackMachineApp.Logic.Atms;
using SnackMachineApp.WinUI.Common;
using SnackMachineApp.Logic.Utils;
using SnackMachineApp.Logic.SharedKernel;

namespace SnackMachineApp.WinUI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private readonly Atm _atm;
        private readonly AtmRepository _repository;
        private readonly PaymentGateway _PaymentGateway;

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
            _repository = new AtmRepository();
            _PaymentGateway = new PaymentGateway();

            TakeMoneyCommand = new Command<decimal>(x => x > 0, Withdrawal);
        }

        private void Withdrawal(decimal amount)
        {
            if(!_atm.CanWithdrawal(amount))
            {
                Message = _atm.Project();
                return;
            }

            _atm.Withdrawal(amount);
            var charge = amount + _atm.CalculateCommision(amount);
            _PaymentGateway.ChargePayment(charge);
            _repository.Save(_atm);

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

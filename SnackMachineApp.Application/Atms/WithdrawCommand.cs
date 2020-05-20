using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;

namespace SnackMachineApp.Application.Atms
{
    public class WithdrawCommand : ICommand<Atm>
    {
        public WithdrawCommand(long atmId, decimal amount)
        {
            AtmId = atmId;
            Amount = amount;
        }

        public long AtmId { get; }
        public decimal Amount { get; }
    }

    internal class WithdrawCommandHandler : ICommandHandler<WithdrawCommand, Atm>
    {
        private readonly IAtmRepository _atmRepository;
        private readonly IPaymentGateway _paymentGateway;

        public WithdrawCommandHandler(IAtmRepository atmRepository, IPaymentGateway paymentGateway)
        {
            _atmRepository = atmRepository;
            _paymentGateway = paymentGateway;
        }

        public Atm Handle(WithdrawCommand request)
        {
            var atm = _atmRepository.GetById(request.AtmId);
            if (atm.Withdraw(request.Amount))
            {
                var charge = request.Amount + atm.CalculateCommision(request.Amount);

                _paymentGateway.ChargePayment(charge);

                _atmRepository.Save(atm);
            }

            return atm;
        }
    }
}

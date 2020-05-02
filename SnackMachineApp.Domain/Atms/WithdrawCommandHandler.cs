using SnackMachineApp.Domain.Core.Interfaces;
using SnackMachineApp.Domain.Utils;

namespace SnackMachineApp.Domain.Atms
{
    public class WithdrawCommand : IRequest<Atm>
    {
        public WithdrawCommand(Atm atm, decimal amount)
        {
            Atm = atm;
            Amount = amount;
        }

        public Atm Atm { get; }
        public decimal Amount { get; }
    }

    internal class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Atm>
    {
        private readonly IComponentLocator componentLocator;

        public WithdrawCommandHandler(IComponentLocator componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        public Atm Handle(WithdrawCommand request)
        {
            if (request.Atm.CanWithdrawal(request.Amount))
            {
                request.Atm.Withdrawal(request.Amount);
                var charge = request.Amount + request.Atm.CalculateCommision(request.Amount);

                var paymentGateway = componentLocator.Resolve<IPaymentGateway>();
                paymentGateway.ChargePayment(charge);

                var atmRepository = componentLocator.Resolve<IAtmRepository>();
                atmRepository.Save(request.Atm);
            }

            return request.Atm;
        }
    }
}

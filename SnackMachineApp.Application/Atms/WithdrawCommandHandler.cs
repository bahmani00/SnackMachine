using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure;

namespace SnackMachineApp.Application.Atms
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
        public Atm Handle(WithdrawCommand request)
        {
            if (request.Atm.CanWithdrawal(request.Amount))
            {
                request.Atm.Withdrawal(request.Amount);
                var charge = request.Amount + request.Atm.CalculateCommision(request.Amount);

                var paymentGateway = ObjectFactory.Instance.Resolve<IPaymentGateway>();
                paymentGateway.ChargePayment(charge);

                var atmRepository = ObjectFactory.Instance.Resolve<IAtmRepository>();
                atmRepository.Save(request.Atm);
            }

            return request.Atm;
        }
    }
}

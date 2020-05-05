using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

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

                var paymentGateway = Infrastructure.ObjectFactory.Instance.Resolve<IPaymentGateway>();
                paymentGateway.ChargePayment(charge);

                //Save(request);
                SaveInScope(request);
            }

            return request.Atm;
        }
        private void Save(WithdrawCommand request)
        {
            var atmRepository = Infrastructure.ObjectFactory.Instance.Resolve<IAtmRepository>();
            atmRepository.Save(request.Atm);
        }

        private void SaveInScope(WithdrawCommand request)
        {
            using (var scope = Infrastructure.ObjectFactory.Instance.CreateScope())
            using (var transaction = scope.ServiceProvider.GetService<ITransactionUnitOfWork>())
            using (var unitOfWork = transaction.BeginTransaction())
            {
                try
                {
                    var atmRepository = scope.ServiceProvider.GetService<IAtmRepository>();
                    atmRepository.Save(request.Atm);
                    //throw new System.Exception();
                }
                catch (System.Exception exc)
                {
                    unitOfWork.Rollback();
                }
            }
        }

    }
}

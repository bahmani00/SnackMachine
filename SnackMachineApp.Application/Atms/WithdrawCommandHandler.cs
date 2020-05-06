using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using System;

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

                SaveAtm(request);
            }

            return request.Atm;
        }

        private void SaveAtm(WithdrawCommand request)
        {
            using (var scope = new DatabaseScope())
            {
                scope.Execute(() => {
                    try
                    {
                        var atmRepository = scope.ServiceProvider.GetService<IAtmRepository>();
                        atmRepository.Save(request.Atm);
                    }
                    catch (Exception exc)
                    {
                        exc.ToString();

                        //TODO: do sth with exc
                    }
                });
            }
        }

    }
}

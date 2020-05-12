using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using System;

namespace SnackMachineApp.Application.Atms
{
    internal class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Atm>
    {
        private readonly IServiceProvider serviceProvider;

        public WithdrawCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Atm Handle(WithdrawCommand request)
        {
            var atmRepository = serviceProvider.GetService<IAtmRepository>();
            var atm = atmRepository.GetById(request.AtmId);
            if (atm.CanWithdraw(request.Amount))
            {
                atm.Withdraw(request.Amount);
                var charge = request.Amount + atm.CalculateCommision(request.Amount);

                var paymentGateway = serviceProvider.GetService<IPaymentGateway>();
                paymentGateway.ChargePayment(charge);

                UpdateAtm(atm);
            }

            return atm;
        }

        private void UpdateAtm(Atm atm)
        {
            using (var scope = new DatabaseScope(serviceProvider))
            {
                scope.Execute(() => {
                    try
                    {
                        var atmRepository = scope.GetService<IAtmRepository>();
                        atmRepository.Save(atm);
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

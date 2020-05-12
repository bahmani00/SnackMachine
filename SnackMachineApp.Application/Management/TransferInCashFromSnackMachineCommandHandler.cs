using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SnackMachines;
using System;

namespace SnackMachineApp.Application.Management
{
    public class TransferInCashFromSnackMachineCommandHandler : IRequestHandler<TransferInCashFromSnackMachineCommand, HeadOffice>
    {
        private readonly IServiceProvider serviceProvider;

        public TransferInCashFromSnackMachineCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HeadOffice Handle(TransferInCashFromSnackMachineCommand request)
        {
            var snackMachineRepository = serviceProvider.GetService<ISnackMachineRepository>();
            var snackMachine = snackMachineRepository.GetById(request.SnackMachineId);
            if (snackMachine == null)
                return null;

            var headOfficeRepository = serviceProvider.GetService<IHeadOfficeRepository>();
            var headOffice = headOfficeRepository.GetById(request.HeadOfficeId);

            using (var scope = new DatabaseScope(serviceProvider))
            {
                scope.Execute(() => {
                    try
                    {
                        snackMachineRepository = scope.GetService<ISnackMachineRepository>();
                        headOfficeRepository = scope.GetService<IHeadOfficeRepository>();

                        headOffice.TransferInCashFromSnackMachine(snackMachine);
                        snackMachineRepository.Save(snackMachine);
                        headOfficeRepository.Save(headOffice);
                    }
                    catch (Exception exc)
                    {
                        exc.ToString();

                        //TODO: do sth with exc
                    }
                });
            }

            return headOffice;
        }
    }
}

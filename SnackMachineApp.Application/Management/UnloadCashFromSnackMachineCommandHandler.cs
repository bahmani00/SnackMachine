using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SnackMachines;
using System;

namespace SnackMachineApp.Application.Management
{
    public class UnloadCashFromSnackMachineCommandHandler : IRequestHandler<UnloadCashFromSnackMachineCommand, HeadOffice>
    {
        private readonly IServiceProvider serviceProvider;

        public UnloadCashFromSnackMachineCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HeadOffice Handle(UnloadCashFromSnackMachineCommand request)
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

                        headOffice.UnloadCashFromSnackMachine(snackMachine);
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

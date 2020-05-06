using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SnackMachines;
using System;

namespace SnackMachineApp.Application.Management
{
    public class UnloadCashFromSnackMachineCommandHandler : IRequestHandler<UnloadCashFromSnackMachineCommand, HeadOffice>
    {
        public HeadOffice Handle(UnloadCashFromSnackMachineCommand request)
        {
            var snackMachineRepository = Infrastructure.ObjectFactory.Instance.Resolve<ISnackMachineRepository>();
            var snackMachine = snackMachineRepository.GetById(request.SnackMachineId);
            if (snackMachine == null)
                return null;

            var headOffice = HeadOfficeInstance.Instance;

            using (var scope = new DatabaseScope())
            {
                scope.Execute(() => {
                    try
                    {
                        snackMachineRepository = scope.ServiceProvider.GetService<ISnackMachineRepository>();
                        var headOfficeRepository = scope.ServiceProvider.GetService<IHeadOfficeRepository>();

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

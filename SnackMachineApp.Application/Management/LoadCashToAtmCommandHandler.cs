using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using System;

namespace SnackMachineApp.Application.Management
{
    public class LoadCashToAtmCommandHandler : IRequestHandler<LoadCashToAtmCommand, HeadOffice>
    {
        public HeadOffice Handle(LoadCashToAtmCommand request)
        {
            var atmRepository = Infrastructure.ObjectFactory.Instance.Resolve<IAtmRepository>();
            var atm = atmRepository.GetById(request.AtmId);

            if (atm == null)
                return null;

            var headOffice = HeadOfficeInstance.Instance;

            using (var scope = new DatabaseScope())
            {
                scope.Execute(() => {
                    try
                    {
                        atmRepository = scope.ServiceProvider.GetService<IAtmRepository>();
                        var headOfficeRepository = scope.ServiceProvider.GetService<IHeadOfficeRepository>();

                        headOffice.LoadCashToAtm(atm);
                        atmRepository.Save(atm);
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

﻿using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using System;

namespace SnackMachineApp.Application.Management
{
    public class LoadCashToAtmCommandHandler : IRequestHandler<LoadCashToAtmCommand, HeadOffice>
    {
        private readonly IServiceProvider serviceProvider;

        public LoadCashToAtmCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HeadOffice Handle(LoadCashToAtmCommand request)
        {
            var headOfficeRepository = serviceProvider.GetService<IHeadOfficeRepository>();
            var headOffice = headOfficeRepository.GetById(request.HeadOfficeId);

            using (var scope = new DatabaseScope(serviceProvider))
            {
                scope.Execute(() => {
                    try
                    {
                        var atmRepository = serviceProvider.GetService<IAtmRepository>();
                        var atm = atmRepository.GetById(request.AtmId);

                        atmRepository = scope.GetService<IAtmRepository>();
                        headOfficeRepository = scope.GetService<IHeadOfficeRepository>();

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

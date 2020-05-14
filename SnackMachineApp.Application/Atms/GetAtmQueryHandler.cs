using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure.Data;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace SnackMachineApp.Application.Atms
{
    internal class GetAtmQueryHandler : IRequestHandler<GetAtmQuery, Atm>
    {
        private readonly IServiceProvider serviceProvider;

        public GetAtmQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Atm Handle(GetAtmQuery request)
        {
            var repository = serviceProvider.GetService<IAtmRepository>();
            return repository.GetById(request.AtmId);
        }
    }
}
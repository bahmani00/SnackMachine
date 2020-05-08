using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using System;

namespace SnackMachineApp.Application.SnackMachines
{
    internal class GetSnackMachineQueryHandler : IRequestHandler<GetSnackMachineQuery, SnackMachine>
    {
        private readonly IServiceProvider serviceProvider;

        public GetSnackMachineQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public SnackMachine Handle(GetSnackMachineQuery request)
        {
            var repository = serviceProvider.GetService<ISnackMachineRepository>();
            return repository.GetById(request.SnackMachineId);
        }
    }
}

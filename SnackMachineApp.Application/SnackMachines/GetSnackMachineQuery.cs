using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using System;

namespace SnackMachineApp.Application.SnackMachines
{
    public class GetSnackMachineQuery : IQuery<SnackMachine>
    {
        public GetSnackMachineQuery(long snackMachineId)
        {
            SnackMachineId = snackMachineId;
        }

        public long SnackMachineId { get; }
    }

    internal class GetSnackMachineQueryHandler : IQueryHandler<GetSnackMachineQuery, SnackMachine>
    {
        private readonly IServiceProvider serviceProvider;

        public GetSnackMachineQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public SnackMachine Handle(GetSnackMachineQuery request)
        {
            using (var repository = serviceProvider.GetService<ISnackMachineRepository>())
                return repository.GetById(request.SnackMachineId);
        }
    }
}

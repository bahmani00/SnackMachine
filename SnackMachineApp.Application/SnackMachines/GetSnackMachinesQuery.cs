using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Application.SnackMachines
{
    public class GetSnackMachinesQuery : IQuery<IReadOnlyList<SnackMachineDto>>
    {
    }

    internal class GetSnackMachinesQueryHandler : IQueryHandler<GetSnackMachinesQuery, IReadOnlyList<SnackMachineDto>>
    {
        private readonly IServiceProvider serviceProvider;

        public GetSnackMachinesQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IReadOnlyList<SnackMachineDto> Handle(GetSnackMachinesQuery request)
        {
            using (var dapper = serviceProvider.GetService<DapperRepositor1y>())
            {
                return dapper.Query<SnackMachineDto>($"SELECT *, Amount as MoneyInside FROM {nameof(SnackMachine)}")
                    .ToList().AsReadOnly();
            }
        }
    }

}

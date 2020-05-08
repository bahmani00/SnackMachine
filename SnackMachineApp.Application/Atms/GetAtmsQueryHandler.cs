using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Application.Atms
{
    internal class GetAtmsQueryHandler : IRequestHandler<GetAtmsQuery, IReadOnlyList<AtmDto>>
    {
        private readonly IServiceProvider serviceProvider;

        public GetAtmsQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IReadOnlyList<AtmDto> Handle(GetAtmsQuery request)
        {
            using (var dapper = serviceProvider.GetService<DapperRepositor1y>())
            {
                return dapper.Query<AtmDto>($"SELECT * FROM {nameof(Atm)}")
                    .ToList().AsReadOnly();
            }
        }
    }
}

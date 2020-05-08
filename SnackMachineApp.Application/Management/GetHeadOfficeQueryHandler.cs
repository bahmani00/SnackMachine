using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Application.Management
{
    internal class GetHeadOfficeQueryHandler : IRequestHandler<GetHeadOfficeQuery, HeadOffice>
    {
        private readonly IServiceProvider serviceProvider;

        public GetHeadOfficeQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HeadOffice Handle(GetHeadOfficeQuery request)
        {
            using (var dapper = serviceProvider.GetService<DapperRepositor1y>())
            {
                var repository = serviceProvider.GetService<IHeadOfficeRepository>();
                return repository.GetById(request.HeadOfficeId);
                //Func<Atm, Money, Atm> func = (a, m) => { a.MoneyInside = g; return a; };
                //return dapper.Query<Atm, Money>($"SELECT * FROM {nameof(Atm)} WHERE {nameof(Atm)}Id={request.AtmId}", func);
            }
        }
    }
}

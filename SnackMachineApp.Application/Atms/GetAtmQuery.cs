using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using System;

namespace SnackMachineApp.Application.Atms
{
    public class GetAtmQuery : IQuery<Atm>
    {
        public GetAtmQuery(long atmId)
        {
            AtmId = atmId;
        }

        public long AtmId { get; }
    }

    internal class GetAtmQueryHandler : IQueryHandler<GetAtmQuery, Atm>
    {
        private readonly IServiceProvider serviceProvider;

        public GetAtmQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Atm Handle(GetAtmQuery request)
        {
            using (var repository = serviceProvider.GetService<IAtmRepository>())
                return repository.GetById(request.AtmId);
        }
    }
}

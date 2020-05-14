using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SeedWork;
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
            var repository = serviceProvider.GetService<IRepository<HeadOffice>>();

            return repository.GetById(request.HeadOfficeId);
        }
    }
}

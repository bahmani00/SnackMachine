using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;
using System.Collections.Generic;

namespace SnackMachineApp.Application.SnackMachines
{
    internal class GetSnackMachinesQueryHandler : IRequestHandler<GetSnackMachinesQuery, IReadOnlyList<SnackMachineDto>>
    {
        public IReadOnlyList<SnackMachineDto> Handle(GetSnackMachinesQuery request)
        {
            var repository = ObjectFactory.Instance.Resolve<ISnackMachineRepository>();
            return repository.GetAll();
        }
    }
}

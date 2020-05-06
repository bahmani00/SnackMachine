using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;

namespace SnackMachineApp.Application.SnackMachines
{
    internal class GetSnackMachineQueryHandler : IRequestHandler<GetSnackMachineQuery, SnackMachine>
    {
        public SnackMachine Handle(GetSnackMachineQuery request)
        {
            var repository = ObjectFactory.Instance.Resolve<ISnackMachineRepository>();
            return repository.GetById(request.SnackMachineId);
        }
    }
}

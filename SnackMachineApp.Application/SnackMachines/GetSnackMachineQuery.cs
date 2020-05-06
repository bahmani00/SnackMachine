using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class GetSnackMachineQuery : IRequest<SnackMachine>
    {
        public GetSnackMachineQuery(long snackMachineId)
        {
            SnackMachineId = snackMachineId;
        }

        public long SnackMachineId { get; }
    }
}

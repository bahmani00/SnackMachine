using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class ReturnMoneyCommand : IRequest<SnackMachine>
    {
        public ReturnMoneyCommand(SnackMachine snackMachine)
        {
            SnackMachine = snackMachine;
        }

        public SnackMachine SnackMachine { get; }
    }
}

using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class BuySnackCommand : IRequest<SnackMachine>
    {
        public BuySnackCommand(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
        }

        public SnackMachine SnackMachine { get; }
        public int Position { get; }
    }
}

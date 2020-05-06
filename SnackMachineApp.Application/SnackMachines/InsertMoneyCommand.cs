using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class InsertMoneyCommand : IRequest<SnackMachine>
    {
        public InsertMoneyCommand(SnackMachine snackMachine, Money coinOrNote)
        {
            SnackMachine = snackMachine;
            CoinOrNote = coinOrNote;
        }

        public SnackMachine SnackMachine { get; }
        public Money CoinOrNote { get; }
    }
}

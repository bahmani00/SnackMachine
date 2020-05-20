using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class InsertMoneyCommand : ICommand<SnackMachine>
    {
        public InsertMoneyCommand(SnackMachine snackMachine, Money coinOrNote)
        {
            SnackMachine = snackMachine;
            CoinOrNote = coinOrNote;
        }

        public SnackMachine SnackMachine { get; }
        public Money CoinOrNote { get; }
    }

    internal class InsertMoneyCommandHandler : ICommandHandler<InsertMoneyCommand, SnackMachine>
    {
        public SnackMachine Handle(InsertMoneyCommand request)
        {
            request.SnackMachine.InsertMoney(request.CoinOrNote);

            return request.SnackMachine;
        }
    }
}
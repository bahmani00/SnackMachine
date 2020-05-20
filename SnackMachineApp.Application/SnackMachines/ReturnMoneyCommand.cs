using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class ReturnMoneyCommand : ICommand<SnackMachine>
    {
        public ReturnMoneyCommand(SnackMachine snackMachine)
        {
            SnackMachine = snackMachine;
        }

        public SnackMachine SnackMachine { get; }
    }

    internal class ReturnMoneyCommandHandler : ICommandHandler<ReturnMoneyCommand, SnackMachine>
    {
        public SnackMachine Handle(ReturnMoneyCommand request)
        {
            request.SnackMachine.ReturnMoney();

            return request.SnackMachine;
        }
    }
}

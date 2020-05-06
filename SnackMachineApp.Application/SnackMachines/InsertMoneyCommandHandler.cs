using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    internal class InsertMoneyCommandHandler : IRequestHandler<InsertMoneyCommand, SnackMachine>
    {
        public SnackMachine Handle(InsertMoneyCommand request)
        {
            request.SnackMachine.InsertMoney(request.CoinOrNote);

            return request.SnackMachine;
        }
    }
}

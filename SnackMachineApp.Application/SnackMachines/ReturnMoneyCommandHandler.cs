using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    internal class ReturnMoneyCommandHandler : IRequestHandler<ReturnMoneyCommand, SnackMachine>
    {
        public SnackMachine Handle(ReturnMoneyCommand request)
        {
            request.SnackMachine.ReturnMoney();

            return request.SnackMachine;
        }
    }
}

using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class BuySnackCommand : ICommand<SnackMachine>
    {
        public BuySnackCommand(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
        }

        public SnackMachine SnackMachine { get; }
        public int Position { get; }
    }

    internal class BuySnackCommandHandler : ICommandHandler<BuySnackCommand, SnackMachine>
    {
        private readonly ISnackMachineRepository _snackMachineRepository;

        public BuySnackCommandHandler(ISnackMachineRepository snackMachineRepository)
        {
            this._snackMachineRepository = snackMachineRepository;
        }

        public SnackMachine Handle(BuySnackCommand request)
        {
            if (request.SnackMachine.BuySnack(request.Position))
            {
                _snackMachineRepository.Save(request.SnackMachine);
            }

            return request.SnackMachine;
        }
    }
}

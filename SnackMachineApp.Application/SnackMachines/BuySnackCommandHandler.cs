using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure;

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

    internal class BuySnackCommandHandler : IRequestHandler<BuySnackCommand, SnackMachine>
    {
        public SnackMachine Handle(BuySnackCommand request)
        {
            if (request.SnackMachine.CanBuySnack(request.Position))
            {
                request.SnackMachine.BuySnack(request.Position);
                var repository = ObjectFactory.Instance.Resolve<ISnackMachineRepository>();
                repository.Save(request.SnackMachine);
            }

            return request.SnackMachine;
        }
    }
}

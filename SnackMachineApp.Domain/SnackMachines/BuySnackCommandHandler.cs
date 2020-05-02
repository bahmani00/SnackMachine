using SnackMachineApp.Domain.Core.Interfaces;
using SnackMachineApp.Domain.Utils;

namespace SnackMachineApp.Domain.SnackMachines
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
        private readonly IComponentLocator componentLocator;

        public BuySnackCommandHandler(IComponentLocator componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        public SnackMachine Handle(BuySnackCommand request)
        {
            if (request.SnackMachine.CanBuySnack(request.Position))
            {
                request.SnackMachine.BuySnack(request.Position);
                var repository = componentLocator.Resolve<ISnackMachineRepository>();
                repository.Save(request.SnackMachine);
            }

            return request.SnackMachine;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using System;

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

                using (var scope = new DatabaseScope())
                {
                    scope.Execute(() => {
                        try
                        {
                            var repository = scope.ServiceProvider.GetService<ISnackMachineRepository>();
                            repository.Save(request.SnackMachine);
                        }
                        catch (Exception exc)
                        {
                            exc.ToString();

                            //TODO: do sth with exc
                        }
                    });
                }
            }

            return request.SnackMachine;
        }
    }
}

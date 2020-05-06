using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;

namespace SnackMachineApp.Application.Atms
{
    public class WithdrawCommand : IRequest<Atm>
    {
        public WithdrawCommand(Atm atm, decimal amount)
        {
            Atm = atm;
            Amount = amount;
        }

        public Atm Atm { get; }
        public decimal Amount { get; }
    }
}

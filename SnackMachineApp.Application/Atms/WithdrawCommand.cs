using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;

namespace SnackMachineApp.Application.Atms
{
    public class WithdrawCommand : IRequest<Atm>
    {
        public WithdrawCommand(long atmId, decimal amount)
        {
            AtmId = atmId;
            Amount = amount;
        }

        public long AtmId { get; }
        public decimal Amount { get; }
    }
}

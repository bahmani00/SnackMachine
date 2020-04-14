using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.SharedKernel;

namespace SnackMachineApp.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {
        public virtual decimal Balance { get; protected set; }
        public virtual Money Cash { get; protected set; } = Money.None;

    }
}

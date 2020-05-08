using SnackMachineApp.Domain.SeedWork;
using System;

namespace SnackMachineApp.Domain.Atms
{
    public class BalanceChangedEvent : DomainEventBase
    {
        public decimal Delta { get; }
        public long TargetId { get; }

        public BalanceChangedEvent(decimal delta, long targetId)
        {
            Delta = delta;
            TargetId = targetId;
        }
    }
}

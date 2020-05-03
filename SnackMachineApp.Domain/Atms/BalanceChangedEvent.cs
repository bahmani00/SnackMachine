using SnackMachineApp.Domain.Seedwork;
using System;

namespace SnackMachineApp.Domain.Atms
{
    public class BalanceChangedEvent : DomainEventBase
    {
        public decimal Delta { get; }

        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}

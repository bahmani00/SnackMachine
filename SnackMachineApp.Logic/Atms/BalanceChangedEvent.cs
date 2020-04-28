using SnackMachineApp.Logic.Core.Interfaces;
using System;

namespace SnackMachineApp.Logic.Atms
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public DateTime DateOccurred { get; } = DateTime.Now;
        public decimal Delta { get; }

        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}

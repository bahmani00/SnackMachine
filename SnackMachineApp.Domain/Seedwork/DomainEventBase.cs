using SnackMachineApp.Domain.SharedKernel;
using System;

namespace SnackMachineApp.Domain.SeedWork
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            DateOccurred = SystemClock.Now;
        }

        public DateTime DateOccurred { get; }
    }
}
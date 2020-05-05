using System;

namespace SnackMachineApp.Domain.SeedWork
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            DateOccurred = DateTime.Now;
        }

        public DateTime DateOccurred { get; }
    }
}
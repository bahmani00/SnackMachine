using System;

namespace SnackMachineApp.Domain.Seedwork
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
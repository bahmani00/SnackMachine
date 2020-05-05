using System;

namespace SnackMachineApp.Domain.SeedWork
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}

using System;

namespace SnackMachineApp.Domain.Core.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}

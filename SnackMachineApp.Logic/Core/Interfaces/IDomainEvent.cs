using System;

namespace SnackMachineApp.Logic.Core.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}

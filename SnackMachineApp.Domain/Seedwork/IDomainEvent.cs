using System;

namespace SnackMachineApp.Domain.Seedwork
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}

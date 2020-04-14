using System;

namespace SnackMachineApp.Logic.Core
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; set; }
    }
}

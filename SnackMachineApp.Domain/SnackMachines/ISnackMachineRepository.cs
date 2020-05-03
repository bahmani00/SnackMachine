using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;

namespace SnackMachineApp.Domain.SnackMachines
{
    public interface ISnackMachineRepository : IRepository<SnackMachine>
    {
        IReadOnlyList<SnackMachineDto> GetAll();
    }
}

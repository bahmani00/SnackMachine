using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.SnackMachines
{
    public interface ISnackMachineRepository : IRepository<SnackMachine>
    {
        IReadOnlyList<SnackMachineDto> GetAll();
    }

    internal class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        public IReadOnlyList<SnackMachineDto> GetAll()
        {
            //TODO(performance issue): This gets all SnackMachine and then for each SnackMachine calls all its childeren
            return this.List()
                .Select(SnackMachineDto.From)
                .ToList();
        }
    }
}

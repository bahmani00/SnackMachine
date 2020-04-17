using SnackMachineApp.Logic.Core;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.SnackMachines
{
    public class SnackMachineRepository : Repository<SnackMachine>
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

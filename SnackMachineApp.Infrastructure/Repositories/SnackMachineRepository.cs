using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Repositories
{

    internal class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        public IReadOnlyList<SnackMachineDto> GetAll()
        {
            //TODO(performance issue): This gets all SnackMachine and then for each SnackMachine calls all its childeren
            return List()
                .Select(SnackMachineDto.From)
                .ToList();
        }
    }
}

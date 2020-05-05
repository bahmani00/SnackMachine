using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Repositories
{

    internal class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        public SnackMachineRepository(ITransactionUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public IReadOnlyList<SnackMachineDto> GetAll()
        {
            //TODO(performance issue): This gets all SnackMachine and then for each SnackMachine calls all its childeren
            return List()
                .Select(SnackMachineDto.From)
                .ToList();
        }
    }
}

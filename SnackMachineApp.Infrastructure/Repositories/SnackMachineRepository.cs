using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;

namespace SnackMachineApp.Infrastructure.Repositories
{
    internal class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        public SnackMachineRepository(ITransactionUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }
    }
}

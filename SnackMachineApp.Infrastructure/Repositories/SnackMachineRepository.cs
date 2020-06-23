using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Infrastructure.Repositories
{
    internal class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        public override SnackMachine GetById(long id)
        {
            var entity = base.GetById(id);


            return entity;
        }
    }
}

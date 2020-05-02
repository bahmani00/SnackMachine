using SnackMachineApp.Domain.Core;
using SnackMachineApp.Domain.SeedWork;

namespace SnackMachineApp.Domain.Management
{
    public interface IHeadOfficeRepository : IRepository<HeadOffice>
    {
    }

    internal class HeadOfficeRepository : Repository<HeadOffice>, IHeadOfficeRepository
    {
    }
}

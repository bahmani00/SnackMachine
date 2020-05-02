using SnackMachineApp.Domain.Core;
using SnackMachineApp.Domain.Core.Interfaces;

namespace SnackMachineApp.Domain.Management
{
    public interface IHeadOfficeRepository : IRepository<HeadOffice>
    {
    }

    internal class HeadOfficeRepository : Repository<HeadOffice>, IHeadOfficeRepository
    {
    }
}

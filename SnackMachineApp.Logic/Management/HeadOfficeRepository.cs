using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.Core.Interfaces;

namespace SnackMachineApp.Logic.Management
{
    public interface IHeadOfficeRepository : IRepository<HeadOffice>
    {
    }

    public class HeadOfficeRepository : Repository<HeadOffice>, IHeadOfficeRepository
    {
    }
}

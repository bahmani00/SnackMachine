using SnackMachineApp.Domain.Management;
using SnackMachineApp.Infrastructure.Data;

namespace SnackMachineApp.Infrastructure.Repositories
{
    internal class HeadOfficeRepository : Repository<HeadOffice>, IHeadOfficeRepository
    {
        public HeadOfficeRepository(ITransactionUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}

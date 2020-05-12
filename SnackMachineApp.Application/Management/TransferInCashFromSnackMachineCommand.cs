using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;

namespace SnackMachineApp.Application.Management
{
    public class TransferInCashFromSnackMachineCommand : IRequest<HeadOffice>
    {
        public TransferInCashFromSnackMachineCommand(long snackMachineId, long headOfficeId)
        {
            SnackMachineId = snackMachineId;
            HeadOfficeId = headOfficeId;
        }

        public long SnackMachineId { get; }
        public long HeadOfficeId { get; }
    }
}

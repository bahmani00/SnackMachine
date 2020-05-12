using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;

namespace SnackMachineApp.Application.Management
{
    public class TransferCashToAtmCommand: IRequest<HeadOffice>
    {
        public TransferCashToAtmCommand(long headOfficeId, long atmId)
        {
            HeadOfficeId = headOfficeId;
            AtmId = atmId;
        }

        public long HeadOfficeId { get; }
        public long AtmId { get; }
    }
}

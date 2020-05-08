using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;

namespace SnackMachineApp.Application.Management
{
    public class GetHeadOfficeQuery : IRequest<HeadOffice>
    {
        public GetHeadOfficeQuery(long headOfficeId)
        {
            HeadOfficeId = headOfficeId;
        }

        public long HeadOfficeId { get; }
    }
}

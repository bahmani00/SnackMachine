using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;

namespace SnackMachineApp.Application.Atms
{
    public class GetAtmQuery : IRequest<Atm>
    {
        public GetAtmQuery(long atmId)
        {
            AtmId = atmId;
        }

        public long AtmId { get; }
    }
}

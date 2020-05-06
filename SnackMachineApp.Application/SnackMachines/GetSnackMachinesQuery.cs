using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using System.Collections.Generic;

namespace SnackMachineApp.Application.SnackMachines
{
    public class GetSnackMachinesQuery : IRequest<IReadOnlyList<SnackMachineDto>>
    {
    }
}

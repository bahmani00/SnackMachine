using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using System.Collections.Generic;

namespace SnackMachineApp.Application.Atms
{
    public class GetAtmsQuery : IRequest<IReadOnlyList<AtmDto>>
    {
    }
}

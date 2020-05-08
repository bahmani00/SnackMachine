using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;

namespace SnackMachineApp.Domain.Atms
{
    public interface IAtmRepository: IRepository<Atm>
    {
        //IReadOnlyList<Atm> GetAll();
    }
}

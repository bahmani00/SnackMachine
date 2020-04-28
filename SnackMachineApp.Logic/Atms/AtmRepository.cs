using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.Atms
{
    public interface IAtmRepository: IRepository<Atm>
    {
        IReadOnlyList<AtmDto> GetAll();
    }

    public class AtmRepository : Repository<Atm>, IAtmRepository
    {
        public IReadOnlyList<AtmDto> GetAll()
        {
            return this.List()
                .Select(AtmDto.From)
                .ToList();
        }
    }
}

using SnackMachineApp.Domain.Core;
using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Domain.Atms
{
    public interface IAtmRepository: IRepository<Atm>
    {
        IReadOnlyList<AtmDto> GetAll();
    }

    internal class AtmRepository : Repository<Atm>, IAtmRepository
    {
        public IReadOnlyList<AtmDto> GetAll()
        {
            return this.List()
                .Select(AtmDto.From)
                .ToList();
        }
    }
}

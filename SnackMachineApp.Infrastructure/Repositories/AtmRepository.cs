using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Repositories
{
    internal class AtmRepository : Repository<Atm>, IAtmRepository
    {
        public IReadOnlyList<AtmDto> GetAll()
        {
            return List()
                .Select(AtmDto.From)
                .ToList();
        }
    }
}

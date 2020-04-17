using SnackMachineApp.Logic.Core;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.Atms
{
    public class AtmRepository : Repository<Atm>
    {
        public IReadOnlyList<AtmDto> GetAll()
        {
            return this.List()
                .Select(AtmDto.From)
                .ToList();
        }
    }
}

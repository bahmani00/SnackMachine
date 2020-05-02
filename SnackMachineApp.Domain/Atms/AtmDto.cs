using System;

namespace SnackMachineApp.Domain.Atms
{
    public class AtmDto
    {
        public long Id { get; private set; }
        public decimal Cash { get; private set; }

        public AtmDto(long id, decimal cash)
        {
            Id = id;
            Cash = cash;
        }

        public static AtmDto From(Atm atm)
        {
            return new AtmDto(atm.Id, atm.MoneyInside.Amount);
        }
    }
}

using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.SnackMachines
{
    public class SnackMachineDto
    {
        public long SnackMachineId { get; private set; }
        public decimal MoneyInside { get; private set; }

        public SnackMachineDto()
        {

        }

        public SnackMachineDto(long id, decimal moneyInside)
        {
            SnackMachineId = id;
            MoneyInside = moneyInside;
        }

    }
}

namespace SnackMachineApp.Domain.SnackMachines
{
    public class SnackMachineDto
    {
        public long Id { get; private set; }
        public decimal MoneyInside { get; private set; }

        public SnackMachineDto(long id, decimal moneyInside)
        {
            Id = id;
            MoneyInside = moneyInside;
        }

        public static SnackMachineDto From(SnackMachine snackMachine)
        {
            return new SnackMachineDto(snackMachine.Id, snackMachine.MoneyInside.Amount);
        }
    }
}

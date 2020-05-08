namespace SnackMachineApp.Application.Atms
{
    public class AtmDto
    {
        public long AtmId { get; private set; }
        public decimal Cash { get; private set; }

        public AtmDto()
        {

        }

        public AtmDto(long id, decimal cash)
        {
            AtmId = id;
            Cash = cash;
        }
    }
}

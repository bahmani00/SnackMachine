namespace SnackMachineApp.Logic
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
        }
    }
}
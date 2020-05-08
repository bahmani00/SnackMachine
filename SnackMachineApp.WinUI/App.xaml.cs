using SnackMachineApp.Infrastructure;
using SnackMachineApp.Infrastructure.Data;

namespace SnackMachineApp.WinUI
{
    public partial class App
    {
        public App()
        {
            //Init object factory
            ObjectFactory.Instance.GetType();
            //DatabasePopulator.PopulateDatabase();
        }
    }
}

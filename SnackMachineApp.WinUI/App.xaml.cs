using SnackMachineApp.Logic.Utils;
using System.Configuration;

namespace SnackMachineApp.Logic.UI
{
    public partial class App
    {
        public App()
        {
            ContainerSetup.Init(ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString);

            ContainerSetup.DisplayRegistrations();
        }
    }
}

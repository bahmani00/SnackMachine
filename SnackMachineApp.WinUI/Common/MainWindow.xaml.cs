using SnackMachineApp.Application.Seedwork;

namespace SnackMachineApp.WinUI.Common
{
    public partial class MainWindow
    {
        public MainWindow(IMediator mediator)
        {
            InitializeComponent();

            DataContext = new MainViewModel(mediator);
        }
    }
}

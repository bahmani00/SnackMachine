using SnackMachineApp.WinUI.Management;

namespace SnackMachineApp.WinUI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            var viewModel = new DashboardViewModel();

            _dialogService.ShowDialog(viewModel);
        }
    }
}

using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.WinUI.Management;

namespace SnackMachineApp.WinUI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(IMediator mediator)
        {
            var viewModel = new DashboardViewModel(mediator);

            _dialogService.ShowDialog(viewModel);
        }
    }
}

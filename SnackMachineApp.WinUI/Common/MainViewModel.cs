using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.Management;
using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.WinUI.Atms;
using SnackMachineApp.WinUI.Management;
using SnackMachineApp.WinUI.SnackMachines;

namespace SnackMachineApp.WinUI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            //var snackMachine = new SnackMachineRepository().Get(1);

            //var viewModel = new SnackMachineViewModel(snackMachine);
            //var atm = new AtmRepository().GetById(1);
            //var viewModel = new AtmViewModel(atm);

            var viewModel = new DashboardViewModel();

            _dialogService.ShowDialog(viewModel);
        }
    }
}

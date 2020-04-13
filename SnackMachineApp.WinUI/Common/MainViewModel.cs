using SnackMachineApp.Logic.Atms;
using SnackMachineApp.Logic.SnackMachines;
using SnackMachineApp.WinUI.Atms;
using SnackMachineApp.WinUI.SnackMachines;

namespace SnackMachineApp.WinUI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            //var snackMachine = new SnackMachineRepository().Get(1);

            //var viewModel = new SnackMachineViewModel(snackMachine);
            Atm atm = new AtmRepository().Get(1);
            var viewModel = new AtmViewModel(atm);

            _dialogService.ShowDialog(viewModel);
        }
    }
}

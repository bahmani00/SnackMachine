using SnackMachineApp.Logic;

namespace SnackMachineApp.WinUI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            var snackMachine = new SnackMachineRepository().Get(1);

            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}

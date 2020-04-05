using SnackMachineApp.Logic;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            SnackMachineApp.Logic.SnackMachine snackMachine;
            using (var session = SessionFactory.OpenSession())
                snackMachine = session.Get<SnackMachineApp.Logic.SnackMachine>(1);

            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}

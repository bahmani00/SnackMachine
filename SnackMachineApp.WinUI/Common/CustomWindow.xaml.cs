namespace SnackMachineApp.WinUI.Common
{
    public partial class CustomWindow
    {
        public CustomWindow(ViewModel viewModel)
        {
            InitializeComponent();

            //Owner = Application.Current.MainWindow;
            DataContext = viewModel;
        }
    }
}

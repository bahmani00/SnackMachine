using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Infrastructure.IoC;
using SnackMachineApp.WinUI.Common;
using System.Windows;

namespace SnackMachineApp.WinUI
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceProvider = ContainerSetup.Init();
            var mediator = serviceProvider.GetService<IMediator>();

            var mainWindow = new MainWindow(mediator);
            mainWindow.ShowDialog();
        }
    }
}

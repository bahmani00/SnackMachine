using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Infrastructure.IoC;
using SnackMachineApp.WinUI.Common;
using System.Configuration;
using System.Windows;

namespace SnackMachineApp.WinUI
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var connectionString = ConfigurationManager.ConnectionStrings["AppCnn"].ConnectionString;
            var ioCContainer = ConfigurationManager.AppSettings["IoCContainer"];
            var dbORM = ConfigurationManager.AppSettings["ORM"];

            var serviceProvider = ContainerSetup.Init(ioCContainer, connectionString, dbORM);
            var mediator = serviceProvider.GetService<IMediator>();

            var mainWindow = new MainWindow(mediator);
            mainWindow.ShowDialog();
        }
    }
}

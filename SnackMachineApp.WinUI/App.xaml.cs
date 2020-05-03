using SnackMachineApp.Infrastructure;
using SnackMachineApp.Infrastructure.Data;

namespace SnackMachineApp.WinUI
{
    public partial class App
    {
        public App()
        {
            //Init object factory
            ObjectFactory.Instance.GetType();
            DatabasePopulator.PopulateDatabase();

            using (var scope = ObjectFactory.Instance.CreateScope())
            {
                //var processor = scope.ServiceProvider.GetService<IUnitOfWork>();
                //processor.Handle(theEvent);
            }
        }
    }
}

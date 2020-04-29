using SnackMachineApp.Logic.Utils;

namespace SnackMachineApp.Logic.UI
{
    public partial class App
    {
        public App()
        {
            //Init object factory
            ObjectFactory.Instance.GetType();

            using (var scope = ObjectFactory.Instance.CreateScope())
            {
                //var processor = scope.ServiceProvider.GetService<IUnitOfWork>();
                //processor.Handle(theEvent);
            }
        }
    }
}

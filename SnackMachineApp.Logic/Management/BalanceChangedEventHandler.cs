using Ardalis.GuardClauses;
using SnackMachineApp.Logic.Core;
using SnackMachineApp.Logic.Management;
using SnackMachineApp.Logic.Utils;
using System.Threading.Tasks;

namespace SnackMachineApp.Logic.Atms
{
    public class BalanceChangedEventHandler : IDomainEventHandler<BalanceChangedEvent>
    {
        public Task Handle(BalanceChangedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            var repository = ObjectFactory.Instance.Resolve<IHeadOfficeRepository>();
            HeadOffice headOffice = HeadOfficeInstance.Instance;
            headOffice.ChangeBalance(domainEvent.Delta);
            repository.Save(headOffice);
   
            return Task.CompletedTask;
        }
    }
}

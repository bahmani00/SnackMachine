using Ardalis.GuardClauses;
using SnackMachineApp.Domain.Core;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.Utils;
using System.Threading.Tasks;

namespace SnackMachineApp.Domain.Atms
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

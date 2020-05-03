using Ardalis.GuardClauses;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Infrastructure;
using System.Threading.Tasks;

namespace SnackMachineApp.Application.Management
{
    public class BalanceChangedEventHandler : IDomainEventHandler<BalanceChangedEvent>
    {
        public Task Handle(BalanceChangedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            var repository = ObjectFactory.Instance.Resolve<IHeadOfficeRepository>();
            var headOffice = HeadOfficeInstance.Instance;
            headOffice.ChangeBalance(domainEvent.Delta);
            repository.Save(headOffice);

            return Task.CompletedTask;
        }
    }
}

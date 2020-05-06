using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Infrastructure;
using SnackMachineApp.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace SnackMachineApp.Application.Management
{
    public class BalanceChangedDomainEventHandler : IDomainEventHandler<BalanceChangedEvent>
    {
        public Task Handle(BalanceChangedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            using (var scope = Infrastructure.ObjectFactory.Instance.CreateScope())
            using (var transaction = scope.ServiceProvider.GetService<ITransactionUnitOfWork>())
            using (var unitOfWork = transaction.BeginTransaction())
            {
                try
                {
                    var repository = scope.ServiceProvider.GetService<IHeadOfficeRepository>();
                    var headOffice = HeadOfficeInstance.Instance;
                    headOffice.ChangeBalance(domainEvent.Delta);
                    repository.Save(headOffice);
                }
                catch (Exception exc)
                {
                    unitOfWork.Rollback();
                }
            }
            return Task.CompletedTask;
        }
    }
}

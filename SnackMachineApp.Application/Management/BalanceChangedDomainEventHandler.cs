using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SnackMachineApp.Application.Management
{
    public class BalanceChangedDomainEventHandler : IDomainEventHandler<BalanceChangedEvent>
    {
        private readonly IServiceProvider serviceProvider;

        public BalanceChangedDomainEventHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task Handle(BalanceChangedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));
           
            using (var scope = serviceProvider.CreateScope())
            using (var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>())
            {
                try
                {
                    var headOfficeRepository = scope.ServiceProvider.GetService<IHeadOfficeRepository>();
                    var headOffice = headOfficeRepository.GetById(domainEvent.TargetId);

                    headOffice.ChangeBalance(domainEvent.Delta);
                    headOfficeRepository.Save(headOffice);

                    unitOfWork.Commit();
                }
                catch(Exception exc)
                {
                    exc.ToString();
                    //TODO: do sth with exc
                }
            }

            return Task.CompletedTask;
        }
    }
}

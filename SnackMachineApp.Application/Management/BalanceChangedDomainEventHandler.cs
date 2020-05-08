using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;
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

            using (var scope = new DatabaseScope(serviceProvider))
            {
                scope.Execute(() => {
                    try
                    {
                        var repository = serviceProvider.GetService<IHeadOfficeRepository>();
                        var headOffice = repository.GetById(domainEvent.TargetId);
                        
                        repository = scope.GetService<IHeadOfficeRepository>();
                        headOffice.ChangeBalance(domainEvent.Delta);
                        repository.Save(headOffice);
                    }
                    catch (Exception exc)
                    {
                        exc.ToString();

                        //TODO: do sth with exc
                    }
                });
            }

            return Task.CompletedTask;
        }
    }
}

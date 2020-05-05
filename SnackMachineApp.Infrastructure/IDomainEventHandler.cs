using SnackMachineApp.Domain.SeedWork;
using System.Threading.Tasks;

namespace SnackMachineApp.Infrastructure
{
    public interface IDomainEventHandler
    {
    }

    public interface IDomainEventHandler<in T>: IDomainEventHandler where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
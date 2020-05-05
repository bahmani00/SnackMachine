using SnackMachineApp.Domain.SeedWork;
using System.Threading.Tasks;

namespace SnackMachineApp.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
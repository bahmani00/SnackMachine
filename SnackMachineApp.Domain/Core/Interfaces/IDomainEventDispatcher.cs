using System.Threading.Tasks;

namespace SnackMachineApp.Domain.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
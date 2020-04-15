using System.Threading.Tasks;

namespace SnackMachineApp.Logic.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
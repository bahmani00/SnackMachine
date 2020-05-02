using SnackMachineApp.Domain.Core.Interfaces;
using System.Threading.Tasks;

namespace SnackMachineApp.Domain.Core
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
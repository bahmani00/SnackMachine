using SnackMachineApp.Logic.Core.Interfaces;
using System.Threading.Tasks;

namespace SnackMachineApp.Logic.Core
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
using System.Threading.Tasks;

namespace SnackMachineApp.Domain.Seedwork
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
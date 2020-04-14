//using System.Threading.Tasks;

//namespace SnackMachineApp.Logic.Core
//{

//    public interface IDomainEventHandler
//    {
//        Task Handle(IDomainEvent domainEvent);
//    }

//    public class DomainEventHandler<T> : IDomainEventHandler
//        where T : IDomainEvent
//    {
//        private readonly IDomainEventHandler<T> _handler;

//        public DomainEventHandler(IDomainEventHandler<T> handler)
//        {
//            _handler = handler;
//        }

//        public override Task Handle(BaseDomainEvent domainEvent)
//        {
//            return _handler.Handle((T)domainEvent);
//        }

//    }
//}
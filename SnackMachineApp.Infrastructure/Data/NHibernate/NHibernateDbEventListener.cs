using NHibernate.Event;
using SnackMachineApp.Domain.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateDbEventListener :
        IPostInsertEventListener,
        IPostDeleteEventListener,
        IPostUpdateEventListener,
        IPostCollectionUpdateEventListener
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public NHibernateDbEventListener(IDomainEventDispatcher domainEventDispatcher)
        {
            this._domainEventDispatcher = domainEventDispatcher;
        }

        public void OnPostUpdate(PostUpdateEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostDelete(PostDeleteEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostInsert(PostInsertEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostUpdateCollection(PostCollectionUpdateEvent ev)
        {
            DispatchEvents(ev.AffectedOwnerOrNull as AggregateRoot);
        }

        private void DispatchEvents(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                return;

            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                _domainEventDispatcher.Dispatch(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }

        #region Async
        public Task OnPostInsertAsync(PostInsertEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostUpdateAsync(PostUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostDeleteAsync(PostDeleteEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        #endregion        
    }
}

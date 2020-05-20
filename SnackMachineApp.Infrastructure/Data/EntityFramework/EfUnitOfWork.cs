using SnackMachineApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Data.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public EfUnitOfWork(DbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            Context = context;
            this._domainEventDispatcher = domainEventDispatcher;
        }

        public void Commit()
        {
            //commit on dispose if any changes have not been committed or rolled back
            if (Context.ChangeTracker.Entries()
                .Any(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
            {
                Context.SaveChanges();

                DispatchEvents();
            }
        }

        private void DispatchEvents()
        {
            var entities = Context.ChangeTracker
                .Entries()
                .Where(x => x.Entity is Entity)
                .Select(x => (Entity)x.Entity)
                .ToList();

            foreach (var entity in entities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    _domainEventDispatcher.Dispatch(domainEvent);
                }
                entity.ClearEvents();
            }
        }

        public void Rollback()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            Context.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Rollback();
        }
        #endregion
    }
}

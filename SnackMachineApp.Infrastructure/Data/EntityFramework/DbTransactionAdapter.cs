using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Domain.SeedWork;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Data.EntityFramework
{
    public class DbTransactionAdapter : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DbTransactionAdapter(DbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            this._context = context;
            this._domainEventDispatcher = domainEventDispatcher;
        }

        public void Commit()
        {
            //commit on dispose if any changes have not been committed or rolled back
            if (_context.ChangeTracker.Entries()
                .Any(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
            {
                _context.SaveChanges();

                DispatchEvents(_context);
            }
        }

        private void DispatchEvents(DbContext context)
        {
            var entities = context.ChangeTracker
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
            _context.ChangeTracker.Entries()
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
            Commit();
        }
        #endregion
    }
}

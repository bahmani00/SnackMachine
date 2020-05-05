using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Domain.SeedWork;
using System.Linq;

namespace SnackMachineApp.Infrastructure.Data.EntityFramework
{
    public class DbTransactionAdapter : IUnitOfWork
    {
        private readonly DbContext _context;

        public DbTransactionAdapter(DbContext context)
        {
            this._context = context;
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

            var eventDispatcher = ObjectFactory.Instance.Resolve<IDomainEventDispatcher>();

            foreach (var entity in entities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    eventDispatcher.Dispatch(domainEvent);
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

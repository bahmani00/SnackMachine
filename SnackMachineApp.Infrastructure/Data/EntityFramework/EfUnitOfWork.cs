using SnackMachineApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace SnackMachineApp.Infrastructure.Data.EntityFramework
{
    public class EfUnitOfWork : ITransactionUnitOfWork
    {
        public DbContext Context { get; }
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public EfUnitOfWork(DbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            Context = context;
            this._domainEventDispatcher = domainEventDispatcher;
        }

        public IUnitOfWork BeginTransaction()
        {
            return new DbTransactionAdapter(Context, _domainEventDispatcher);
        }

        public void Dispose()
        {
        }
    }
}

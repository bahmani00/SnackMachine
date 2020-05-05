using SnackMachineApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace SnackMachineApp.Infrastructure.Data.EntityFramework
{
    public class EfUnitOfWork : ITransactionUnitOfWork
    {
        public DbContext Context { get; }

        public EfUnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
        }

        public IUnitOfWork BeginTransaction()
        {
            return new DbTransactionAdapter(Context);
        }
    }
}

using SnackMachineApp.Domain.Seedwork;
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
            //Context?.Dispose();
        }

        public IUnitOfWork BeginTransaction()
        {
            return new DbTransactionAdapter(Context);
        }
    }
}

using NHibernate;
using SnackMachineApp.Domain.SeedWork;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    public class DbTransactionAdapter : IUnitOfWork
    {
        private readonly ITransaction transaction;

        public DbTransactionAdapter(ITransaction transaction)
        {
            this.transaction = transaction;
        }

        public void Dispose()
        {
            Commit();

            transaction?.Dispose();
        }

        public void Commit()
        {
            if (transaction.IsActive) transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction.IsActive) transaction.Rollback();
        }
    }
}

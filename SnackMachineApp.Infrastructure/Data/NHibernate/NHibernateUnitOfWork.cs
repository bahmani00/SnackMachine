using NHibernate;
using SnackMachineApp.Domain.SeedWork;
using System.Data;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly SessionFactory sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(SessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession Session
        {
            get
            {
                if (_session == null || !_session.IsOpen)
                    _session = sessionFactory.OpenSession();

                return _session;
            }
        }

        internal ITransaction BeginTransaction()
        {
            if (_transaction == null || !_transaction.IsActive)
                _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);

            return _transaction;
        }

        public void Commit()
        {
            if (_transaction?.IsActive == true) _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction?.IsActive == true) _transaction.Rollback();
        }

        public void Dispose()
        {
            Rollback();

            _transaction?.Dispose();
            _session?.Dispose();
        }
    }
}
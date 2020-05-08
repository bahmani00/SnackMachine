using NHibernate;
using SnackMachineApp.Domain.SeedWork;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateUnitOfWork : ITransactionUnitOfWork
    {
        private ISession _session;
        private readonly SessionFactory sessionFactory;

        public NHibernateUnitOfWork(SessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession Session
        {
            get
            {
                if (_session == null || !_session.IsOpen)
                    //_session = ObjectFactory.Instance.Resolve<SessionFactory>().OpenSession();
                    _session = sessionFactory.OpenSession();

                return _session;
            }
        }

        public void Dispose()
        {
            _session?.Dispose();
        }

        public IUnitOfWork BeginTransaction()
        {
            return new DbTransactionAdapter(Session.BeginTransaction());
        }
    }
}
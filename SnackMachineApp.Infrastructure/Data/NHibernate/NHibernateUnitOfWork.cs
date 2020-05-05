using NHibernate;
using SnackMachineApp.Domain.Seedwork;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateUnitOfWork : ITransactionUnitOfWork
    {
        private ISession _session;

        public ISession Session
        {
            get
            {
                return _session == null || !_session.IsOpen
                    ? (_session = ObjectFactory.Instance.Resolve<SessionFactory>().OpenSession())
                    : _session;
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
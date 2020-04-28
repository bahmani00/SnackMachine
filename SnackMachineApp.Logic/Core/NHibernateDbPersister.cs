using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Utils;
using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core
{
    internal class NHibernateDbPersister<T> : IDbPersister<T> where T : AggregateRoot
    {
        private readonly SessionFactory sessionFactory;

        public NHibernateDbPersister(SessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public IList<T> List()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.QueryOver<T>().List<T>();
            };
        }

        public T GetById(long id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public void Save(T entity)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }

    }
}

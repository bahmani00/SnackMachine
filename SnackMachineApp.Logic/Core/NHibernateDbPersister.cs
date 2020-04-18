using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Utils;
using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core
{
    public class NHibernateDbPersister<T> : IRepository<T> where T : AggregateRoot
    {
        public IList<T> List()
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.QueryOver<T>().List<T>();
            };
        }

        public T GetById(long id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public void Save(T entity)
        {
            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }

    }
}

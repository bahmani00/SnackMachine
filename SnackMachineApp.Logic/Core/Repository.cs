using SnackMachineApp.Logic.Core.Interfaces;
using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core
{
    public abstract class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        //TODO: inject the persiter by DI
        private IRepository<T> dbPersister = new NHibernateDbPersister<T>();
        //private IRepository<T> dbPersister = new EFDbPersister<T>();

        public IList<T> List()
        {
            return dbPersister.List();
        }

        public T GetById(long id)
        {
            return dbPersister.GetById(id);
        }

        public void Save(T entity)
        {
            dbPersister.Save(entity);
        }

        public void Delete(T entity)
        {
            dbPersister.Delete(entity);
        }
    }
}

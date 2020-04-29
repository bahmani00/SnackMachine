using SnackMachineApp.Logic.Core.Interfaces;
using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        //TODO: do Field Injection rather Property Injection
        public IDbPersister<T> DbPersister { get; set; }

        public IList<T> List()
        {
            return DbPersister.List();
        }

        public T GetById(long id)
        {
            return DbPersister.GetById(id);
        }

        public void Save(T entity)
        {
            DbPersister.Save(entity);
        }

        public void Delete(T entity)
        {
            DbPersister.Delete(entity);
        }
    }
}

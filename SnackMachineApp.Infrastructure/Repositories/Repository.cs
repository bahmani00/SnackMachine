using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Infrastructure.Data;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        public IDbPersister<T> DbPersister { private get; set; }

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

        public void Dispose()
        {
            DbPersister.Dispose();
        }
    }
}

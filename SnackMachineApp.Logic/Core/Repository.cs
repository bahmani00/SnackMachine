using Autofac;
using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Utils;
using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        //TODO: property injecttion
        private IDbPersister<T> DbPersister { get; set; } = ContainerSetup.Container.Resolve<IDbPersister<T>>();

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

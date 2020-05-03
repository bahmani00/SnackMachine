using System.Collections.Generic;

namespace SnackMachineApp.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        IList<T> List();

        T GetById(long id);

        void Save(T entity);

        void Delete(T entity);
    }
}
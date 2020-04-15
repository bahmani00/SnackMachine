using System.Collections.Generic;

namespace SnackMachineApp.Logic.Core.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T Get(long id);

        IList<T> List();

        void Save(T entity);

        void Delete(T entity);
    }
}
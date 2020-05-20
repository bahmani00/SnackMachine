using SnackMachineApp.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Data
{
    public interface IDbPersister<T> : IDisposable where T : Entity
    {
        IList<T> List();

        T GetById(long id);

        void Save(T entity);

        void Delete(T entity);
    }
}
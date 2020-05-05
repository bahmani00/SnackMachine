using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly ITransactionUnitOfWork _unitOfWork;

        public Repository(ITransactionUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

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

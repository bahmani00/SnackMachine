using NHibernate;
using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateDbPersister<T> : IDbPersister<T> where T : AggregateRoot
    {
        private readonly NHibernateUnitOfWork _unitOfWork;

        public NHibernateDbPersister(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

        protected ISession Session
        {
            get
            {
                return _unitOfWork.Session;
            }
        }

        public IList<T> List()
        {
            return Session.QueryOver<T>().List<T>();
        }

        public T GetById(long id)
        {
            return Session.Get<T>(id);
        }

        public void Save(T entity)
        {
            _unitOfWork.BeginTransaction();
            Session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _unitOfWork.BeginTransaction();
            Session.Delete(entity);
        }

        public void Dispose()
        {
            Session?.Dispose();
            //_unitOfWork?.Dispose();
        }
    }
}
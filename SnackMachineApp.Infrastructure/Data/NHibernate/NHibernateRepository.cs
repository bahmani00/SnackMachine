using NHibernate;
using SnackMachineApp.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    public class NHibernateRepository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly NHibernateUnitOfWork _unitOfWork;

        public NHibernateRepository(ITransactionUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

        //TODO: do Field Injection rather Property Injection
        //public IDbPersister<T> DbPersister { get; set; }

        protected ISession Session
        {
            get
            {
                return _unitOfWork.Session;
            }
        }

        public ITransactionUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
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
            Session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}

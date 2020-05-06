using NHibernate;
using SnackMachineApp.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class NHibernateDbPersister<T> : IDbPersister<T> where T : AggregateRoot
    {
        private readonly NHibernateUnitOfWork _unitOfWork;

        public NHibernateDbPersister(ITransactionUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

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
            //TODO: remove using
            using (Session)
                return Session.QueryOver<T>().List<T>();
        }

        public T GetById(long id)
        {
            //TODO: remove using
            using (Session)
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
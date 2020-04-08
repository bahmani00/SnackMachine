﻿namespace SnackMachineApp.Logic
{
    public class Repository<T> where T: AggregateRoot
    {
        public T Get(int id)
        {
            using(var session = SessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public void Save(T aggregateRoot)
        {
            if (!ValidateBeforeSave(aggregateRoot))
                return;

            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(aggregateRoot);
                transaction.Commit();
            }
        }

        public virtual bool ValidateBeforeSave(T aggregateRoot)
        {
            return true;
        }
    }
}
using SnackMachineApp.Logic.Utils;

namespace SnackMachineApp.Logic.Core
{
    public class Repository<T> where T : AggregateRoot
    {
        public T Get(long id)
        {
            using (var session = SessionFactory.OpenSession())
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

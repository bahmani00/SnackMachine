using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Interface.Data
{
    internal class EFDbPersister<T> : IDbPersister<T> where T : AggregateRoot
    {
        private readonly DbContext _dbContext;

        public EFDbPersister(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(long id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        //TODO: merge it into Save method
        //public T Add<T>(T entity) where T : BaseEntity
        //{
        //    _dbContext.Set<T>().Add(entity);
        //    _dbContext.SaveChanges();

        //    return entity;
        //}

        public void Save(T entity)
        {
            //TODO: implement Add
            //https://stackoverflow.com/questions/15045763/what-does-the-dbcontext-entry-do
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}

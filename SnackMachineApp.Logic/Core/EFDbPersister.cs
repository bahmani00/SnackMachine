using Microsoft.EntityFrameworkCore;
using SnackMachineApp.Logic.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.Core
{
    public class EFDbPersister<T> : IRepository<T> where T : AggregateRoot
    {
        //TODO: inject the AppDbContext by DI
        private readonly AppDbContext _dbContext = new AppDbContext();

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

using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Application
{
    //TODO: choose a better name
    internal class DatabaseScope : IServiceProvider, IDisposable
    {
        private readonly IServiceScope scope;

        public DatabaseScope(IServiceProvider serviceProvider)
        {
            this.scope = serviceProvider.CreateScope();
            //this.ServiceProvider = scope.ServiceProvider;
        }

        //public IServiceProvider ServiceProvider { get; }

        public void Execute(Action action)
        {
            using (var transaction = scope.ServiceProvider.GetService<ITransactionUnitOfWork>())
            using (var unitOfWork = transaction.BeginTransaction())
            {
                try
                {
                    action();
                }
                catch
                {
                    unitOfWork.Rollback();
                    //TODO: do sth with exc
                    throw;
                }
            }
        }

        public object GetService(Type serviceType)
        {
            return scope.ServiceProvider.GetService(serviceType);
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}

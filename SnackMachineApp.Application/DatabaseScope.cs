using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Infrastructure.Data;
using System;

namespace SnackMachineApp.Application
{
    //TODO: choose a better name
    internal class DatabaseScope : IDisposable
    {
        private readonly IServiceScope scope;

        public DatabaseScope()
        {
            this.scope = Infrastructure.ObjectFactory.Instance.CreateScope();
            this.ServiceProvider = scope.ServiceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            scope.Dispose();
        }

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
    }
}

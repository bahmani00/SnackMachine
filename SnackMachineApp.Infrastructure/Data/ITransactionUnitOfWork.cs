using SnackMachineApp.Domain.SeedWork;
using System;

namespace SnackMachineApp.Infrastructure.Data
{
    public interface ITransactionUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
    }
}

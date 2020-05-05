using SnackMachineApp.Domain.Seedwork;
using System;

namespace SnackMachineApp.Infrastructure.Data
{
    public interface ITransactionUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
    }
}

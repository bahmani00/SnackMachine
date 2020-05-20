using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SeedWork;
using System;

namespace SnackMachineApp.Application
{
    internal class UnitOfWorkRequestHandlerDecorator<TCommand, TResponse>: 
        ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkRequestHandlerDecorator(
            ICommandHandler<TCommand, TResponse> decorated,
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        public TResponse Handle(TCommand command)
        {
            TResponse result = default;

            try
            {
                result = _decorated.Handle(command);

                _unitOfWork.Commit();
            }
            catch (Exception exc)
            {
                //TODO: do sth with exc
                //throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }
    }
}
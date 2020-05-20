namespace SnackMachineApp.Application.Seedwork
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
    }
}

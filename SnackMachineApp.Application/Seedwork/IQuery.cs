namespace SnackMachineApp.Application.Seedwork
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}

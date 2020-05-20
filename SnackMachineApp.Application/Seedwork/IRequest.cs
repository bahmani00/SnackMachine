namespace SnackMachineApp.Application.Seedwork
{
    public interface IRequest<out TResponse>
    {
    }

    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }
}

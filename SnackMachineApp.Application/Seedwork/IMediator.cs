namespace SnackMachineApp.Application.Seedwork
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }
}

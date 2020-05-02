using SnackMachineApp.Domain.Utils;
using System;

namespace SnackMachineApp.Domain.Core.Interfaces
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }

    public class Mediator : IMediator
    {
        private readonly IComponentLocator componentLocator;

        public Mediator(IComponentLocator componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            Type type = typeof(IRequestHandler<,>);
            Type[] typeArgs = { request.GetType(), typeof(TResponse) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = componentLocator.Resolve(handlerType);
            return handler.Handle((dynamic)request);
        }

    }
}

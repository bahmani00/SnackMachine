using SnackMachineApp.Infrastructure;
using System;

namespace SnackMachineApp.Application.Seedwork
{
    internal class Mediator : IMediator
    {
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            Type type = typeof(IRequestHandler<,>);
            Type[] typeArgs = { request.GetType(), typeof(TResponse) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = ObjectFactory.Instance.Resolve(handlerType);
            return handler.Handle((dynamic)request);
        }

    }
}

using SnackMachineApp.Infrastructure;
using System;

namespace SnackMachineApp.Application.Seedwork
{
    internal class Mediator : IMediator
    {
        private readonly IServiceProvider serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            Type type = typeof(IRequestHandler<,>);
            Type[] typeArgs = { request.GetType(), typeof(TResponse) };
            Type handlerType = type.MakeGenericType(typeArgs);

            //TODO: remove dynamic and ObjectFactory
            dynamic handler = serviceProvider.GetService(handlerType);
            return handler.Handle((dynamic)request);
        }

    }
}

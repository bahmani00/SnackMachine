using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure;

namespace SnackMachineApp.Application.Atms
{
    internal class GetAtmQueryHandler : IRequestHandler<GetAtmQuery, Atm>
    {
        public Atm Handle(GetAtmQuery request)
        {
            var repository = ObjectFactory.Instance.Resolve<IAtmRepository>();
            return repository.GetById(request.AtmId);
        }
    }
}

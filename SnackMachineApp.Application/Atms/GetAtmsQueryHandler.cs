using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Infrastructure;
using System.Collections.Generic;

namespace SnackMachineApp.Application.Atms
{
    internal class GetAtmsQueryHandler : IRequestHandler<GetAtmsQuery, IReadOnlyList<AtmDto>>
    {
        public IReadOnlyList<AtmDto> Handle(GetAtmsQuery request)
        {
            var repository = ObjectFactory.Instance.Resolve<IAtmRepository>();
            return repository.GetAll();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.SnackMachines;
using SnackMachineApp.Infrastructure.Data;
using System;
using System.Linq;

namespace SnackMachineApp.Application.SnackMachines
{
    public class GetSnackMachineQuery : IQuery<SnackMachine>
    {
        public GetSnackMachineQuery(long snackMachineId)
        {
            SnackMachineId = snackMachineId;
        }

        public long SnackMachineId { get; }
    }

    internal class GetSnackMachineQueryHandler : IQueryHandler<GetSnackMachineQuery, SnackMachine>
    {
        private readonly IServiceProvider serviceProvider;

        public GetSnackMachineQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public SnackMachine Handle(GetSnackMachineQuery request)
        {
            //using (var dapper = serviceProvider.GetService<DapperRepositor1y>())
            //{
                //using (var multi = dapper.QueryMultiple(@"
                //    SELECT *
                //    FROM SnackMachine sm WHERE sm.SnackMachineId=@SnackMachineId;

                //    SELECT *
                //    FROM Slot sl INNER JOIN Snack sn ON sl.SnackId = sn.SnackId AND sl.SnackMachineId=@SnackMachineId
                //    ", param: new { request.SnackMachineId }))
                //{
                //    var snackMachine = multi.Read<SnackMachineDto>().FirstOrDefault();

                //    snackMachine.Slots = multi.Read<SlotDto>().ToList();

                //    return snackMachine;
                //}
            //}

            using (var repository = serviceProvider.GetService<ISnackMachineRepository>())
                return repository.GetById(request.SnackMachineId);
        }
    }
}
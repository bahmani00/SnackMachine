using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application.Management
{
    public class TransferInCashFromSnackMachineCommand : ICommand<HeadOffice>
    {
        public TransferInCashFromSnackMachineCommand(long snackMachineId, long headOfficeId)
        {
            SnackMachineId = snackMachineId;
            HeadOfficeId = headOfficeId;
        }

        public long SnackMachineId { get; }
        public long HeadOfficeId { get; }
    }

    internal class TransferInCashFromSnackMachineCommandHandler : ICommandHandler<TransferInCashFromSnackMachineCommand, HeadOffice>
    {
        private readonly IHeadOfficeRepository _headOfficeRepository;
        private readonly ISnackMachineRepository _snackMachineRepository;

        public TransferInCashFromSnackMachineCommandHandler(
            IHeadOfficeRepository headOfficeRepository, 
            ISnackMachineRepository snackMachineRepository)
        {
            _headOfficeRepository = headOfficeRepository;
            _snackMachineRepository = snackMachineRepository;
        }

        public HeadOffice Handle(TransferInCashFromSnackMachineCommand request)
        {
            var snackMachine = _snackMachineRepository.GetById(request.SnackMachineId);
            var headOffice = _headOfficeRepository.GetById(request.HeadOfficeId);

            headOffice.TransferInCashFromSnackMachine(snackMachine);
            _snackMachineRepository.Save(snackMachine);
            _headOfficeRepository.Save(headOffice);

            return headOffice;
        }
    }

}

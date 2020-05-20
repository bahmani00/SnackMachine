using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.Management;

namespace SnackMachineApp.Application.Management
{
    public class TransferCashToAtmCommand: ICommand<HeadOffice>
    {
        public TransferCashToAtmCommand(long headOfficeId, long atmId)
        {
            HeadOfficeId = headOfficeId;
            AtmId = atmId;
        }

        public long HeadOfficeId { get; }
        public long AtmId { get; }
    }

    internal class TransferCashToAtmCommandHandler : ICommandHandler<TransferCashToAtmCommand, HeadOffice>
    {
        private readonly IHeadOfficeRepository _headOfficeRepository;
        private readonly IAtmRepository _atmRepository;

        public TransferCashToAtmCommandHandler(
            IHeadOfficeRepository headOfficeRepository,
            IAtmRepository atmRepository)
        {
            _headOfficeRepository = headOfficeRepository;
            _atmRepository = atmRepository;
        }

        public HeadOffice Handle(TransferCashToAtmCommand request)
        {
            var headOffice = _headOfficeRepository.GetById(request.HeadOfficeId);
            var atm = _atmRepository.GetById(request.AtmId);

            headOffice.TransferCashToAtm(atm);

            _atmRepository.Save(atm);
            _headOfficeRepository.Save(headOffice);

            return headOffice;
        }
    }
}

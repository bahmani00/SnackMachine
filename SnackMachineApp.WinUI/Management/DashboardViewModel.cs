using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Atms;
using SnackMachineApp.Application.Management;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Application.SnackMachines;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.Utils;
using SnackMachineApp.WinUI.Atms;
using SnackMachineApp.WinUI.Common;
using SnackMachineApp.WinUI.SnackMachines;
using System.Collections.Generic;
using System.Windows;

namespace SnackMachineApp.WinUI.Management
{
    public class DashboardViewModel : ViewModel
    {
        private readonly IMediator _mediator;

        public HeadOffice HeadOffice { get; private set; }
        public IReadOnlyList<SnackMachineDto> SnackMachines { get; private set; }
        public IReadOnlyList<AtmDto> Atms { get; private set; }
        public Command<SnackMachineDto> ShowSnackMachineCommand { get; private set; }
        public Command<SnackMachineDto> UnloadCashCommand { get; private set; }
        public Command<AtmDto> ShowAtmCommand { get; private set; }
        public Command<AtmDto> LoadCashToAtmCommand { get; private set; }

        public DashboardViewModel()
        {
            _mediator = Infrastructure.ObjectFactory.Instance.GetService<IMediator>();
            HeadOffice = _mediator.Send(new GetHeadOfficeQuery(Constants.HeadOfficeId));

            RefreshAll();

            ShowSnackMachineCommand = new Command<SnackMachineDto>(x => x != null, ShowSnackMachine);
            UnloadCashCommand = new Command<SnackMachineDto>(CanUnloadCash, UnloadCash);
            ShowAtmCommand = new Command<AtmDto>(x => x != null, ShowAtm);
            LoadCashToAtmCommand = new Command<AtmDto>(CanLoadCashToAtm, LoadCashToAtm);
        }

        private bool CanLoadCashToAtm(AtmDto atmDto)
        {
            return atmDto != null && HeadOffice.Cash.Amount > 0;
        }

        private void LoadCashToAtm(AtmDto atmDto)
        {
            HeadOffice = _mediator.Send(new LoadCashToAtmCommand(HeadOffice.Id, atmDto.AtmId));

            RefreshAll();
        }

        private void ShowAtm(AtmDto atmDto)
        {
            var atm = _mediator.Send(new GetAtmQuery(atmDto.AtmId));

            if (atm == null)
                return;

            _dialogService.ShowDialog(new AtmViewModel(atm));
            RefreshAll();
        }

        private bool CanUnloadCash(SnackMachineDto snackMachineDto)
        {
            return snackMachineDto != null && snackMachineDto.MoneyInside > 0;
        }

        private void UnloadCash(SnackMachineDto snackMachineDto)
        {
            HeadOffice = _mediator.Send(new UnloadCashFromSnackMachineCommand(snackMachineDto.SnackMachineId, HeadOffice.Id));

            RefreshAll();
        }

        private void ShowSnackMachine(SnackMachineDto snackMachineDto)
        {
            var snackMachine = _mediator.Send(new GetSnackMachineQuery(snackMachineDto.SnackMachineId));

            if (snackMachine == null)
            {
                MessageBox.Show("Snack machine was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dialogService.ShowDialog(new SnackMachineViewModel(snackMachine));
            RefreshAll();
        }

        private void RefreshAll()
        {
            SnackMachines = _mediator.Send(new GetSnackMachinesQuery());
            Atms = _mediator.Send(new GetAtmsQuery());

            Notify(nameof(Atms));
            Notify(nameof(SnackMachines));
            Notify(nameof(HeadOffice));
        }
    }
}

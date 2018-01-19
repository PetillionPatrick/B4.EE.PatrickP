using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels.Afkeur
{
    public class StartAfkeurMachineViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private IMachineServices machineService;
        private Beuk currentBeuk;
        private Status currentStatus;

        public StartAfkeurMachineViewModel(IBeukServices beukService, IMachineServices machineService)
        {
            this.beukService = beukService;
            this.machineService = machineService;
        }

        private ObservableCollection<Machine> machines;
        public ObservableCollection<Machine> Machines
        {
            get { return machines; }
            set
            {
                machines = value;
                RaisePropertyChanged(nameof(Machines));
            }
        }

        public async override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;

            base.Init(initData);
            await RefreshMachineList();
        }

        public ICommand OpenMachinePageCommand => new Command<Machine>(async (Machine machine) =>
        {
            currentStatus.Machine = machine;
            currentStatus.MachineId = machine.Id;
            await CoreMethods.PushPageModel<StartAfkeurOperatorViewModel>(currentStatus, false, true);
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshMachineList();
        }

        private async Task RefreshMachineList()
        {
            currentBeuk = new Beuk();
            currentBeuk.Id = currentStatus.Machine.BeukId;
            var machines = await machineService.GetMachineListAsync(currentBeuk.Id);
            Machines = null;
            Machines = new ObservableCollection<Machine>(machines);
        }
    }
}

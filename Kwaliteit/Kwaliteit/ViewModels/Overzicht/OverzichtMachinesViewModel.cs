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

namespace Kwaliteit.ViewModels
{
    public class OverzichtMachinesViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private IMachineServices machineService;
        private IStatusServices statusService;
        private Beuk currentBeuk;

        public OverzichtMachinesViewModel(IBeukServices beukService, IMachineServices machineService, IStatusServices statusService)
        {
            this.beukService = beukService;
            this.machineService = machineService;
            this.statusService = statusService;
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
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
            Beuk beuk = initData as Beuk;
            currentBeuk = beuk;
            base.Init(initData);
            await RefreshMachineList();

        }

        public ICommand OpenMachinePageCommand => new Command<Machine>(async (Machine machine) => {
            IsBusy = true;
                machine.Statussen = await statusService.GetStatusListAsync(machine.Id) as List<Status>;

            await CoreMethods.PushPageModel<OverzichtViewModel>(machine, false, true);
            IsBusy = false;
        });
        

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshMachineList();
        }

        private async Task RefreshMachineList()
        {
            IsBusy = true;
            var machines = await machineService.GetMachineListAsync(currentBeuk.Id);
            Machines = null;
            Machines = new ObservableCollection<Machine>(machines);
            IsBusy = false;
        }
    }
}

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
    public class OverzichtViewModel : FreshBasePageModel
    {
        private IMachineServices machineService;
        private IStatusServices statusService;
            private Machine currentMachine;

            public OverzichtViewModel(IStatusServices statusService, IMachineServices machineService)
            {
                this.statusService = statusService;
                this.machineService = machineService;
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


        private ObservableCollection<Status> statussen;
            public ObservableCollection<Status> Statussen
        {
                get { return statussen; }
                set
                {
                statussen = value;
                    RaisePropertyChanged(nameof(Statussen));
                }
            }

            public async override void Init(object initData)
            {
                Machine machine = initData as Machine;
                currentMachine = machine;
                base.Init(initData);
                await RefreshStatusList();
            }

            public ICommand OpenMachinePageCommand => new Command<Status>(async (Status status) => {
                IsBusy = true;
                await CoreMethods.PushPageModel<OverzichtDetailViewModel>(status, false, true);
                IsBusy = false;
            });

            protected async override void ViewIsAppearing(object sender, EventArgs e)
            {
                base.ViewIsAppearing(sender, e);
                await RefreshStatusList();
            }

            private async Task RefreshStatusList()
            {
                IsBusy = true;
                var statussen = await statusService.GetStatusListAsync(currentMachine.Id);
                
                Statussen = null;
                Statussen = new ObservableCollection<Status>(statussen);
            IsBusy = false;
        }
        }
}

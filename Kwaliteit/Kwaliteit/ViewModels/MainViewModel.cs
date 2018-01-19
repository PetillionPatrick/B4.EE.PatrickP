using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Pages.StartProduktie;
using Kwaliteit.ViewModels.Afkeur;
using Kwaliteit.ViewModels.StartProduktie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        public ICommand OpenOverzichtCommand => new Command(async () => { await CoreMethods.PushPageModel<OverzichtBeukenViewModel>(true); });
        public ICommand OpenUpdateStatusCommand => new Command(async () => { await CoreMethods.PushPageModel<UpdateMachineStatusViewModel>(true); });
        public ICommand OpenAfkeurCommand => new Command<Status>(async (Status status) =>
        {
            status = new Status();
            status.GekozenStatus = status.StatusKeuze.ElementAt(4);
            await CoreMethods.PushPageModel<StartAfkeurBeukViewModel>(status, false, true);
        });
        public ICommand OpenBeheerCommand => new Command(async () => { await CoreMethods.PushPageModel<SettingsViewModel>(true); });

    }
}

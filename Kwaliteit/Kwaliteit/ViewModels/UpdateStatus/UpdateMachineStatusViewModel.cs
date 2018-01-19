using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.ViewModels;
using Kwaliteit.ViewModels.StartProduktie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.Pages.StartProduktie
{
    public class UpdateMachineStatusViewModel : FreshBasePageModel
    {
        public ICommand OpenStartCommand => new Command<Status>(async (Status status) => 
        {
            status = new Status();
            status.GekozenStatus = status.StatusKeuze.ElementAt(1);
            await CoreMethods.PushPageModel<StartProduktieBeukViewModel>(status, false, true);
        });

        public ICommand OpenEindeCommand => new Command<Status>(async (Status status) => 
        {
            status = new Status();
            status.GekozenStatus = status.StatusKeuze.ElementAt(3);
            await CoreMethods.PushPageModel<StartProduktieBeukViewModel>(status, false, true);
        });

        public ICommand OpenReparatieCommand => new Command<Status>(async (Status status) => 
        {
            status = new Status();
            status.GekozenStatus = status.StatusKeuze.ElementAt(2);
            await CoreMethods.PushPageModel<StartProduktieBeukViewModel>(status, false, true);
        });
    }
}

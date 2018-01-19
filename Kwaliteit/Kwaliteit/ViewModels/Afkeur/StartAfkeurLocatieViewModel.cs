using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels.Afkeur
{
    public class StartAfkeurLocatieViewModel : FreshBasePageModel
    {
        private IAfkeurServices afkeurService;
        private Status currentStatus;
        private Afkeuren currentAfkeur;

        public StartAfkeurLocatieViewModel(IAfkeurServices afkeurService)
        {
            this.afkeurService = afkeurService;
        }

        public override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;
            base.Init(initData);
        }


        public ICommand SaveLocatiesCommand => new Command(async () => {

            try
            {
                //Locatie nog afwerken

                //Position locatie = new Position();
                //locatie = await afkeurService.HuidigeLocatie();

                //currentAfkeur.Locatie = $"Latidu is {locatie.Latitude} en Longitude is {locatie.Longitude}";

                //currentStatus.Afkeuren.Add(currentAfkeur);
                await CoreMethods.PushPageModel<StartAfkeurUnitsViewModel>(currentStatus,false, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        });
    }
}

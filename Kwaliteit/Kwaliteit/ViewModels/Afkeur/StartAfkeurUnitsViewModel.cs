using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Kwaliteit.ViewModels.Afkeur
{
    public class StartAfkeurUnitsViewModel : FreshBasePageModel
    {
     


        private IAfkeurServices afkeurService;
        private Status currentStatus;
        private Afkeuren currentAfkeur;
        private Unit currentUnit;

        private string unitNaam;

        public string UnitNaam
        {
            get { return unitNaam; }
            set {
                unitNaam = value;
                RaisePropertyChanged(nameof(UnitNaam));
            }
        }


        public StartAfkeurUnitsViewModel(IAfkeurServices afkeurService)
        {
            this.afkeurService = afkeurService;
        }

        public override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;

            UnitNaam ="";
           
            base.Init(initData);
        }

        public ICommand ScanUnitsCommand => new Command(async () => { await CoreMethods.PushPageModel<CustomScanViewModel>(true); });

        public ICommand SaveUnitsCommand => new Command(async () => { await CoreMethods.PushPageModel<AfkeurViewModel>(true); });       
    }
}

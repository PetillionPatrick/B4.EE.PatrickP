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
    public class OverzichtBeukenViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;

        public OverzichtBeukenViewModel(IBeukServices beukService)
        {
            this.beukService = beukService;
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

        private ObservableCollection<Beuk> beuken;
        public ObservableCollection<Beuk> Beuken
        {
            get { return beuken; }
            set
            {
                beuken = value;
                RaisePropertyChanged(nameof(Beuken));
            }
        }

        public ICommand OpenBeukCommand => new Command<Beuk>(async (Beuk beuk) => {
            IsBusy = true;
            await CoreMethods.PushPageModel<OverzichtMachinesViewModel>(beuk, false, true);
            IsBusy = false;
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshBeukList();
        }

        private async Task RefreshBeukList()
        {
            IsBusy = true;
            var beuk = await beukService.GetBeukList(Guid.Empty);
            

            Beuken = null;
            Beuken = new ObservableCollection<Beuk>(beuk);
            IsBusy = false;
        }
    }
}

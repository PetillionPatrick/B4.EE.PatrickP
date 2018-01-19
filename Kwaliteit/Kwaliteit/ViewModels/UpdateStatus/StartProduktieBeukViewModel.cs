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

namespace Kwaliteit.ViewModels.StartProduktie
{
    public class StartProduktieBeukViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private Status currentStatus;

        public StartProduktieBeukViewModel(IBeukServices beukService)
        {
            this.beukService = beukService;
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

        public ICommand OpenBeukCommand => new Command<Beuk>(async (Beuk beuk) => 
        {
            currentStatus.Machine = new Machine();
            currentStatus.Machine.Beuk = beuk;
            currentStatus.Machine.BeukId = beuk.Id;
            await CoreMethods.PushPageModel<StartProduktieMachineViewModel>(currentStatus, false, true);
        });

        public override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;
            base.Init(initData);
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshBeukList();
        }

        private async Task RefreshBeukList()
        {
            var beuk = await beukService.GetBeukList(Guid.Empty);

            Beuken = null;
            Beuken = new ObservableCollection<Beuk>(beuk);
        }
    }
}

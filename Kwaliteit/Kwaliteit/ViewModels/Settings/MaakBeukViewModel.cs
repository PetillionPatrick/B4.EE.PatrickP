using FluentValidation;
using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Kwaliteit.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class MaakBeukViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private IValidator beukValidator;
        private Beuk currentBeuk;

        public MaakBeukViewModel(IBeukServices beukService)
        {
            this.beukService = beukService;
            beukValidator = new BeukValidator();
        }

        private string beukNaam;

        public string BeukNaam
        {
            get { return beukNaam; }
            set
            {
                beukNaam = value;
                RaisePropertyChanged(nameof(BeukNaam));
            }
        }

        private string beukNaamError;

        public string BeukNaamError
        {
            get { return beukNaamError; }
            set
            {
                beukNaamError = value;
                RaisePropertyChanged(nameof(BeukNaamError));
                RaisePropertyChanged(nameof(BeukNaamErrorVisible));
            }
        }

        public bool BeukNaamErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(BeukNaamError); }
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

        private void LoadBeukState()
        {
            BeukNaam = currentBeuk.Naam;
        }

        private void SaveBeukState()
        {
            currentBeuk.Naam = BeukNaam;
        }

        public async override void Init(object initData)
        {
            Beuk beuk = initData as Beuk;
            currentBeuk = beuk;

            LoadBeukState();
            base.Init(initData);
             await RefreshBeukList();
           
            //}
        }

        public ICommand OpenEditBeukCommand => new Command<Beuk>(async (Beuk beuk) => {await CoreMethods.PushPageModel<MaakMachineViewModel>(beuk, false, true);});


        public ICommand SaveBeukCommand => new Command(async () =>
        {
            currentBeuk.Edit = true;
            SaveBeukState();
            

            if (Validate(currentBeuk))
            {
                if (currentBeuk.Id == Guid.Empty)
                {
                    currentBeuk.Id = Guid.NewGuid();
                    currentBeuk.OwnerId = Guid.Empty;
                }
                currentBeuk.Edit = false;
                await beukService.SaveBeuk(currentBeuk, true);
     
                currentBeuk.Id = Guid.Empty;
                currentBeuk.Naam = "";
                LoadBeukState();
                await RefreshBeukList();
            }
        });

        public ICommand DeleteBeukCommand => new Command<Beuk>(async (Beuk beuk) => {
            await beukService.DeleteBeuk(beuk.Id);
            await RefreshBeukList();
        });

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

        private bool Validate(Beuk beuk)
        {
            var validatieResult = beukValidator.Validate(beuk);
            if (beuk.Edit)
            {
                foreach (var error in validatieResult.Errors)
                {
                    if (error.PropertyName == nameof(beuk.Naam))
                    {
                        BeukNaamError = error.ErrorMessage;
                    }
                }
                return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }
    }
}

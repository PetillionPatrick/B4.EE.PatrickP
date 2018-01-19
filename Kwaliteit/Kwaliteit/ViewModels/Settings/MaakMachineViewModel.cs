using FluentValidation;
using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Kwaliteit.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class MaakMachineViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private IMachineServices machineService;
        private IValidator beukValidator;
        private IValidator machineValidator;
        private Beuk currentBeuk;
        private Machine currentMachine;

        public MaakMachineViewModel(IBeukServices beukService, IMachineServices machineService)
        {
            this.beukService = beukService;
            this.machineService = machineService;
            beukValidator = new BeukValidator();
            machineValidator = new MachineValidator();
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

        private string machineNaam;

        public string MachineNaam
        {
            get { return machineNaam; }
            set
            {
                machineNaam = value;
                RaisePropertyChanged(nameof(MachineNaam));
            }
        }

        private string machineNaamError;

        public string MachineNaamError
        {
            get { return machineNaamError; }
            set
            {
                machineNaamError = value;
                RaisePropertyChanged(nameof(MachineNaamError));
                RaisePropertyChanged(nameof(MachineNaamErrorVisible));
            }
        }

        public bool MachineNaamErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(MachineNaamError); }
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

        private void LoadBeukState()
        {
            BeukNaam = currentBeuk.Naam;
        }

        private void SaveBeukState()
        {
            currentBeuk.Naam = BeukNaam;
        }

        private void LoadMachineState()
        {
            MachineNaam = currentMachine.Naam;
        }

        private void SaveMachineState()
        {
            currentMachine.Naam = MachineNaam;
        }

        public async override void Init(object initData)
        {
            Beuk beuk = initData as Beuk;
            currentBeuk = beuk;
            currentMachine = new Machine {Naam = "", Id = Guid.Empty, BeukId = beuk.Id, Edit = false};
            LoadBeukState();
            LoadMachineState();
            base.Init(initData);
            await RefreshMachineList();

            //}
        }

        public ICommand OpenMachineEditPageCommand => new Command<Machine>(async (Machine machine) => { await CoreMethods.PushPageModel<EditMachineViewModel>(machine, false, true);});

        public ICommand SaveMachineCommand => new Command(async () =>
        {
            currentMachine.Edit = true;
            SaveBeukState();
            SaveMachineState();

            if (Validate(currentMachine))
            {
                if (currentMachine.Id == Guid.Empty)
                {
                    currentMachine.Id = Guid.NewGuid();
                    currentMachine.Beuk = currentBeuk;


                }
                currentMachine.Edit = false;
                await machineService.SaveMachine(currentMachine, true);
                currentMachine.Id = Guid.Empty;
                currentMachine.Naam = "";
                LoadMachineState();
                await RefreshMachineList();
            }
        });

        public ICommand SaveBeukCommand => new Command<Beuk>(async (Beuk beuk) => {
            SaveBeukState();
            if (Validate(currentBeuk))
            {
                if (beuk == null)
                {
                    beuk = currentBeuk;
                }
                await beukService.SaveBeuk(beuk);

                await CoreMethods.PopPageModel();
            }
        });

        public ICommand DeleteMachineCommand => new Command<Machine>(async (Machine machine) => {
            await machineService.DeleteMachineAsync(machine.Id);
            await RefreshMachineList();
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshMachineList();
        }

        private async Task RefreshMachineList()
        {
            var machines = await machineService.GetMachineListAsync(currentBeuk.Id);
            Machines = null;
            Machines = new ObservableCollection<Machine>(machines);
        }

        private bool Validate(Beuk beuk)
        {
            var validatieResult = beukValidator.Validate(beuk);

            foreach (var error in validatieResult.Errors)
            {
                if (error.PropertyName == nameof(beuk.Naam))
                {
                    BeukNaamError = error.ErrorMessage;
                }
            }
            return validatieResult.IsValid;
        }

        private bool Validate(Machine machine)
        {
            var validatieResult = machineValidator.Validate(machine);
            if (machine.Edit)
            { 
            foreach (var error in validatieResult.Errors)
            {
                if (error.PropertyName == nameof(machine.Naam))
                {
                    MachineNaamError = error.ErrorMessage;
                }
            }
            return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }
    }
}

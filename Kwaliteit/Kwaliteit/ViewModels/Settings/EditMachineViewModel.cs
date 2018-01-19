using FluentValidation;
using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Kwaliteit.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class EditMachineViewModel : FreshBasePageModel
    {
        private IMachineServices machineService;
        private IValidator machineValidator;
        private Machine currentMachine;

        public EditMachineViewModel(IMachineServices machineService)
        {
            this.machineService = machineService;
            machineValidator = new MachineValidator();
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

        private void LoadMachineState()
        {
            MachineNaam = currentMachine.Naam;
        }

        private void SaveMachineState()
        {
            currentMachine.Naam = MachineNaam;
        }

        public override void Init(object initData)
        {
            Machine machine = initData as Machine;
            currentMachine = machine;
            LoadMachineState();
            base.Init(initData);
        }

        public ICommand SaveMachineCommand => new Command(async () =>
        {
            SaveMachineState();

            if (Validate(currentMachine))
            {
                
                await machineService.SaveMachine(currentMachine);
                await CoreMethods.PopPageModel();
            }
        });

        private bool Validate(Machine machine)
        {
            var validationResult = machineValidator.Validate(machine);

            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(machine.Naam))
                {
                    MachineNaamError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }
    }
}

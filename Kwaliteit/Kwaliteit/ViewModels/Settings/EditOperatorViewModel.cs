﻿using FluentValidation;
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
    public class EditOperatorViewModel : FreshBasePageModel
    {
        private IOperatorServices operatorService;
        private IValidator operatorValidator;
        private Operator currentOperator;
        private LineInspector currentLi;

        public EditOperatorViewModel(IOperatorServices operatorService)
        {
            this.operatorService = operatorService;
            operatorValidator = new OperatorValidator();
        }

        private string operatorNummer;

        public string OperatorNummer
        {
            get { return operatorNummer; }
            set
            {
                operatorNummer = value;
                RaisePropertyChanged(nameof(OperatorNummer));
            }
        }

        private string operatorNummerError;

        public string OperatorNummerError
        {
            get { return operatorNummerError; }
            set
            {
                operatorNummerError = value;
                RaisePropertyChanged(nameof(OperatorNummerError));
                RaisePropertyChanged(nameof(OperatorNummerErrorVisible));
            }
        }

        public bool OperatorNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(OperatorNaamError); }
        }

        private string operatorNaam;

        public string OperatorNaam
        {
            get { return operatorNaam; }
            set
            {
                operatorNaam = value;
                RaisePropertyChanged(nameof(OperatorNaam));
            }
        }

        private string operatorNaamError;

        public string OperatorNaamError
        {
            get { return operatorNaamError; }
            set
            {
                operatorNaamError = value;
                RaisePropertyChanged(nameof(OperatorNaamError));
                RaisePropertyChanged(nameof(OperatorNaamErrorrVisible));
            }
        }

        public bool OperatorNaamErrorrVisible
        {
            get { return !string.IsNullOrWhiteSpace(OperatorNaamError); }
        }

        private ObservableCollection<Operator> operatoren;
        public ObservableCollection<Operator> Operatoren
        {
            get { return operatoren; }
            set
            {
                operatoren = value;
                RaisePropertyChanged(nameof(Operatoren));
            }
        }

        private bool techinsch;

        public bool Techinsch
        {
            get { return techinsch; }
            set { techinsch = value; }
        }



        public async override void Init(object initData)
        {
            Operator ope = initData as Operator;
            currentOperator = ope;

            LoadOperatorState();
            base.Init(initData);
            await RefreshOperatorList();
        }

        private void LoadOperatorState()
        {
            OperatorNaam = currentOperator.Naam;
            OperatorNummer = currentOperator.Nummer;
            Techinsch = currentOperator.Technisch;
        }

        private void SaveOperatorState()
        {
            currentOperator.Naam = OperatorNaam;
            currentOperator.Nummer = OperatorNummer;
            currentOperator.Technisch = Techinsch;
        }

        public ICommand SaveOperatorCommand => new Command(async () =>
        {
            currentOperator.Edit = true;
            SaveOperatorState();


            if (Validate(currentOperator))
            {
                if (currentOperator.Id == Guid.Empty)
                {
                    currentOperator.Id = Guid.NewGuid();
                }
                await operatorService.SaveOperator(currentOperator);
                await CoreMethods.PopPageModel();
            }
        });

        private async Task RefreshOperatorList()
        {
            var ope = await operatorService.GetOperatorListAsync();

            Operatoren = null;
            Operatoren = new ObservableCollection<Operator>(ope);
        }

        private bool Validate(Operator ope)
        {
            var validatieResult = operatorValidator.Validate(ope);
            if (ope.Edit)
            {
                foreach (var error in validatieResult.Errors)
                {
                    if (error.PropertyName == nameof(ope.Naam))
                    {
                        OperatorNaamError = error.ErrorMessage;
                    }

                    if (error.PropertyName == nameof(ope.Nummer))
                    {
                        OperatorNummerError = error.ErrorMessage;
                    }
                }
                return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }
    }
}

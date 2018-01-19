using FluentValidation;
using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Kwaliteit.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class StartMachineViewModel : FreshBasePageModel
    {
        private IBeukServices beukService;
        private IMachineServices machineService;
        private IOperatorServices operatorService;
        private IOrderServices orderService;
        private IUnitServices unitService;
        private IStatusServices statusService;
        private IValidator beukValidator;
        private IValidator machineValidator;
        private IValidator orderValidator;
        private IValidator unitValidator;
        private IValidator statusValidator;
        private Status currentStatus;
        private Unit currentUnit;

        public StartMachineViewModel(IBeukServices beukService, IMachineServices machineService, IOperatorServices operatorService, IOrderServices orderService, IUnitServices unitService, IStatusServices statusService)
        {
            this.beukService = beukService;
            this.machineService = machineService;
            this.operatorService = operatorService;
            this.orderService = orderService;
            this.unitService = unitService;
            this.statusService = statusService;
            beukValidator = new BeukValidator();
            machineValidator = new MachineValidator();
            orderValidator = new StatusValidator();
            unitValidator = new UnitValidator();
            statusValidator = new StatusValidator();
        }

        private string partNummer;

        public string PartNummer
        {
            get { return partNummer; }
            set
            {
                partNummer = value;
                RaisePropertyChanged(nameof(PartNummer));
            }
        }

        private string partNummerError;

        public string PartNummerError
        {
            get { return partNummerError; }
            set
            {
                partNummerError = value;
                RaisePropertyChanged(nameof(PartNummerError));
                RaisePropertyChanged(nameof(PartNummerErrorVisible));
            }
        }

        public bool PartNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string moldNummer;

        public string MoldNummer
        {
            get { return moldNummer; }
            set
            {
                moldNummer = value;
                RaisePropertyChanged(nameof(MoldNummer));
            }
        }

        private string moldNummerError;

        public string MoldNummerError
        {
            get { return moldNummerError; }
            set
            {
                moldNummerError = value;
                RaisePropertyChanged(nameof(MoldNummerError));
                RaisePropertyChanged(nameof(MoldNummerErrorVisible));
            }
        }

        public bool MoldNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string orderNummer;

        public string OrderNummer
        {
            get { return orderNummer; }
            set
            {
                orderNummer = value;
                RaisePropertyChanged(nameof(OrderNummer));
            }
        }

        private string orderNummerError;

        public string OrderNummerError
        {
            get { return orderNummerError; }
            set
            {
                orderNummerError = value;
                RaisePropertyChanged(nameof(OrderNummerError));
                RaisePropertyChanged(nameof(OrderNummerErrorVisible));
            }
        }

        public bool OrderNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string unitNummer;

        public string UnitNummer
        {
            get { return unitNummer; }
            set
            {
                unitNummer = value;
                RaisePropertyChanged(nameof(UnitNummer));
            }
        }

        private string unitNummerError;

        public string UnitNummerError
        {
            get { return unitNummerError; }
            set
            {
                unitNummerError = value;
                RaisePropertyChanged(nameof(UnitNummerError));
                RaisePropertyChanged(nameof(UnitNummerErrorVisible));
            }
        }

        public bool UnitNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string archiefNummer;

        public string ArchiefNummer
        {
            get { return archiefNummer; }
            set
            {
                archiefNummer = value;
                RaisePropertyChanged(nameof(ArchiefNummer));
            }
        }

        private string archiefNummerError;

        public string ArchiefNummerError
        {
            get { return archiefNummerError; }
            set
            {
                archiefNummerError = value;
                RaisePropertyChanged(nameof(ArchiefNummerError));
                RaisePropertyChanged(nameof(ArchiefNummerErrorVisible));
            }
        }

        public bool ArchiefNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string reparatieNummer;

        public string ReparatieNummer
        {
            get { return reparatieNummer; }
            set
            {
                reparatieNummer = value;
                RaisePropertyChanged(nameof(ReparatieNummer));
            }
        }

        private string reparatieNummerError;

        public string ReparatieNummerError
        {
            get { return reparatieNummerError; }
            set
            {
                reparatieNummerError = value;
                RaisePropertyChanged(nameof(ReparatieNummerError));
                RaisePropertyChanged(nameof(ReparatieNummerErrorVisible));
            }
        }

        public bool ReparatieNummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PartNummerError); }
        }

        private string opmerking;

        public string Opmerking
        {
            get { return opmerking; }
            set
            {
                opmerking = value;
                RaisePropertyChanged(nameof(Opmerking));
            }
        }

        private bool delenControle;

        public bool DelenControle
        {
            get { return delenControle; }
            set
            {
                delenControle = value;
                RaisePropertyChanged(nameof(DelenControle));
            }
        }

        private bool proefspuitingControle;

        public bool ProefspuitingControle
        {
            get { return proefspuitingControle; }
            set
            {
                proefspuitingControle = value;
                RaisePropertyChanged(nameof(ProefspuitingControle));
            }
        }

        private void SaveStartProduktieState()
        {
            currentStatus.ArchiefNr = ArchiefNummer;
            currentStatus.PartNummer = PartNummer;
            currentStatus.ProefSpuiting = ProefspuitingControle;
            currentStatus.ReparatieNummer = ReparatieNummer;
            currentStatus.VormNr = MoldNummer;
        }

        private void LoadProduktieStateState()
        {
            ArchiefNummer = "";
            PartNummer = "";
            ReparatieNummer = "";
            MoldNummer = "";
            OrderNummer = "";
            UnitNummer = "";
        }

        public override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;
            LoadProduktieStateState();
            base.Init(initData);
        }

        public ICommand SaveStartCommand => new Command(async () =>
        {
            
            currentStatus.Edit = true;
            SaveStartProduktieState();
            if(Validate(currentStatus))
            {
                Order o = new Order();
                o.Naam = OrderNummer;

                currentUnit = new Unit();
                currentUnit.Naam = UnitNummer;
                currentUnit.Order = o;
                
                currentUnit.StatusId = currentStatus.Id;

                if (Validate(currentUnit))
                {
                    if (currentUnit.Id == Guid.Empty)
                    {

                        o.Id = Guid.NewGuid();
                        

                        await orderService.SaveOrder(o, true);

                        currentUnit.Id = Guid.NewGuid();
                        currentUnit.OrderId = o.Id;
                        currentStatus.Id = Guid.NewGuid();
                        currentStatus.Datum = DateTime.Now;
                        currentStatus.Id = Guid.NewGuid();
                        currentStatus.Edit = false;
                        currentStatus.OrderId = o.Id;
                        
                        await statusService.SaveStatus(currentStatus, true);
                        await unitService.SaveUnit(currentUnit, true);
                        await CoreMethods.PopToRoot(true);
                    }
                                
                }
               
            }
        });

        private bool Validate(Unit unit)
        {
            var validatieResult = unitValidator.Validate(unit);
            if (currentStatus.Edit)
            {
                foreach (var error in validatieResult.Errors)
                {
                    if (error.PropertyName == nameof(unit.Order.Naam))
                    {
                        OrderNummerError = error.ErrorMessage;
                    }
                }
                return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }

        private bool Validate(Status status)
        {
            var validatieResult = statusValidator.Validate(status);
            if (currentStatus.Edit)
            {
                foreach (var error in validatieResult.Errors)
                {
                    if (error.PropertyName == nameof(status.ArchiefNr))
                    {
                        ArchiefNummerError = error.ErrorMessage;
                    }

                    if (error.PropertyName == nameof(status.PartNummer))
                    {
                        PartNummerError = error.ErrorMessage;
                    }

                    if (error.PropertyName == nameof(status.ReparatieNummer))
                    {
                        ReparatieNummerError = error.ErrorMessage;
                    }

                    if (error.PropertyName == nameof(status.VormNr))
                    {
                        MoldNummerError = error.ErrorMessage;
                    }
                }
                return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }
    }
}

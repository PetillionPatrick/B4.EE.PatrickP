using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels.StartProduktie
{
    public class StartProduktieOperatorViewModel : FreshBasePageModel
    {
        private IOperatorServices operatorService;
        private Operator currentOperator;
        private Machine currentMachine;
        private Status currentStatus;

        public StartProduktieOperatorViewModel(IOperatorServices operatorService)
        {
            this.operatorService = operatorService;
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
        



        public async override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;
            currentMachine= currentStatus.Machine;

            base.Init(initData);
            await RefreshOperatorList();

            //}
        }

        

        public ICommand OpenOperatorCommand => new Command<Operator>(async (Operator ope) => {
           
            currentStatus.Operator = ope;
            currentStatus.OperatorId = ope.Id;
            await CoreMethods.PushPageModel<StartProduktieLiViewModel>(currentStatus, false, true);
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshOperatorList();
        }

        private async Task RefreshOperatorList()
        {
            if ((currentStatus.GekozenStatus == currentStatus.StatusKeuze.ElementAt(2)) || (currentStatus.GekozenStatus == currentStatus.StatusKeuze.ElementAt(1)))
            {
                var ope = await operatorService.GetTechnischOpeListAsync();

                Operatoren = null;
                Operatoren = new ObservableCollection<Operator>(ope);
            }
            else
            {
                var ope = await operatorService.GetOperatorListAsync();

                Operatoren = null;
                Operatoren = new ObservableCollection<Operator>(ope);
            }
        }

    }
}

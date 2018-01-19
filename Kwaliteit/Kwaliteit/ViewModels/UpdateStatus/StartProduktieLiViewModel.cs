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
    public class StartProduktieLiViewModel : FreshBasePageModel
    {
        private ILineInspectorServices lineInspectorService;
        private Operator currentLiOperator;
        private Status currentStatus;

        public StartProduktieLiViewModel(ILineInspectorServices lineInspectorService)
        {
            this.lineInspectorService = lineInspectorService;
        }



        private ObservableCollection<LineInspector> lineInspectors;
        public ObservableCollection<LineInspector> LineInspectors
        {
            get { return lineInspectors; }
            set
            {
                lineInspectors = value;
                RaisePropertyChanged(nameof(LineInspectors));
            }
        }




        public async override void Init(object initData)
        {
            Status status = initData as Status;
            currentStatus = status;

            base.Init(initData);
            await RefreshOperatorList();

        }

        public ICommand OpenLiCommand => new Command<LineInspector>(async (LineInspector li) => {

           
            currentStatus.Li = li;
            currentStatus.LiId = li.Id;
            await CoreMethods.PushPageModel<StartMachineViewModel>(currentStatus, false, true);
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshOperatorList();
        }

        private async Task RefreshOperatorList()
        {

            var li = await lineInspectorService.GetLiListAsync();

            LineInspectors = null;
            LineInspectors = new ObservableCollection<LineInspector>(li);
        }
    }
}

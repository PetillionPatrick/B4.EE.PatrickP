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

namespace Kwaliteit.ViewModels.Settings
{
    class MaakLiViewModel : FreshBasePageModel
    {
        private ILineInspectorServices lineInspectorService;
        private IValidator LiValidator;
        private LineInspector currentLi;

        public MaakLiViewModel(ILineInspectorServices lineInspectorService)
        {
            this.lineInspectorService = lineInspectorService;
            LiValidator = new LiValidator();
        }

        private string lineInspectornummer;

        public string LineInspectornummer
        {
            get { return lineInspectornummer; }
            set
            {
                lineInspectornummer = value;
                RaisePropertyChanged(nameof(LineInspectornummer));
            }
        }

        private string lineInspectornummerError;

        public string LineInspectornummerError
        {
            get { return lineInspectornummerError; }
            set
            {
                lineInspectornummerError = value;
                RaisePropertyChanged(nameof(LineInspectornummerError));
                RaisePropertyChanged(nameof(LineInspectornummerErrorVisible));
            }
        }

        public bool LineInspectornummerErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(LineInspectornummerError); }
        }

        private string lineInspectornaam;

        public string LineInspectornaam
        {
            get { return lineInspectornaam; }
            set
            {
                lineInspectornaam = value;
                RaisePropertyChanged(nameof(LineInspectornaam));
            }
        }

        private string lineInspectornaamError;

        public string LineInspectornaamError
        {
            get { return lineInspectornaamError; }
            set
            {
                lineInspectornaamError = value;
                RaisePropertyChanged(nameof(LineInspectornaamError));
                RaisePropertyChanged(nameof(LineInspectornaamErrorrVisible));
            }
        }

        public bool LineInspectornaamErrorrVisible
        {
            get { return !string.IsNullOrWhiteSpace(LineInspectornaamError); }
        }

        private ObservableCollection<LineInspector> lineInspectoren;
        public ObservableCollection<LineInspector> LineInspectoren
        {
            get { return lineInspectoren; }
            set
            {
                lineInspectoren = value;
                RaisePropertyChanged(nameof(LineInspectoren));
            }
        }

        public async override void Init(object initData)
        {
            LineInspector li = initData as LineInspector;
            currentLi = li;

            LoadOperatorState();
            base.Init(initData);
            await RefreshLiList();

            //}
        }

        public ICommand OpenEditOperatorCommand => new Command<LineInspector>(async (LineInspector li) => { await CoreMethods.PushPageModel<EditOperatorViewModel>(li, false, true); });

        private void LoadOperatorState()
        {
            LineInspectornaam = currentLi.Naam;
            LineInspectornummer = currentLi.Nummer;
        }

        private void SaveOperatorState()
        {
            currentLi.Naam = LineInspectornaam;
            currentLi.Nummer = LineInspectornummer;
        }

        public ICommand SaveLiCommand => new Command(async () =>
        {
            currentLi.Edit = true;
            SaveOperatorState();


            if (Validate(currentLi))
            {
                if (currentLi.Id == Guid.Empty)
                {
                    currentLi.Id = Guid.NewGuid();


                }
                currentLi.Edit = false;
                await lineInspectorService.SaveLiAsync(currentLi, true);
                currentLi.Id = Guid.Empty;
                currentLi.Naam = "";
                currentLi.Nummer = "";
                LoadOperatorState();
                await RefreshLiList();
            }
        });

        public ICommand DeleteOperatorCommand => new Command<LineInspector>(async (LineInspector li) => {
            await lineInspectorService.DeleteLiAsync(li.Id);
            await RefreshLiList();
        });

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshLiList();
        }

        private async Task RefreshLiList()
        {
            var li = await lineInspectorService.GetLiListAsync();

            LineInspectoren = null;
            LineInspectoren = new ObservableCollection<LineInspector>(li);
        }

        private bool Validate(LineInspector li)
        {
            var validatieResult = LiValidator.Validate(li);
            if (li.Edit)
            {
                foreach (var error in validatieResult.Errors)
                {
                    if (error.PropertyName == nameof(li.Naam))
                    {
                        LineInspectornaamError = error.ErrorMessage;
                    }

                    if (error.PropertyName == nameof(li.Nummer))
                    {
                        LineInspectornummerError = error.ErrorMessage;
                    }
                }
                return validatieResult.IsValid;
            }
            return validatieResult.IsValid;
        }
    }
}

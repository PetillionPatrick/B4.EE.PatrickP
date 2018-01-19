using FreshMvvm;
using Kwaliteit.Domain.Models;
using Kwaliteit.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kwaliteit.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        public ICommand OpenMaakBeukCommand => new Command<Beuk>(async (Beuk beuk) =>
        {
            if (beuk == null)
            {
                beuk = new Beuk
                {

                    Naam = "",
                    Id = Guid.Empty,
                    OwnerId = Guid.Empty,
                    Owner = new User(),
                    Edit = false,
                    Machines = new List<Machine>()
                };
            }
            await CoreMethods.PushPageModel<MaakBeukViewModel>(beuk, false, true); });

        public ICommand OpenMaakOperatorCommand => new Command<Operator>(async (Operator ope) => 
        {
            if (ope == null)
            {
                ope = new Operator
                {
                    Naam = "",
                    Id = Guid.Empty,
                    Nummer = "",
                    Edit = false
                };
            }
            await CoreMethods.PushPageModel<MaakOperatorViewModel>(ope, false, true); });

        public ICommand OpenMaakLiCommand => new Command<LineInspector>(async (LineInspector li) =>
        {
            if (li == null)
            {
                li = new LineInspector
                {
                    Naam = "",
                    Id = Guid.Empty,
                    Nummer = "",
                    Edit = false
                };
            }
            await CoreMethods.PushPageModel<MaakLiViewModel>(li, false, true);
        });


    }
}

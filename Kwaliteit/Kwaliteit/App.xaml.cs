using FreshMvvm;
using Kwaliteit.Domain.Services.Abstract;
using Kwaliteit.Domain.Services.API;
using Kwaliteit.Domain.Services.SQLite;
using Kwaliteit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Kwaliteit
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            FreshIOC.Container.Register<IBeukServices>(new BeukAPIService());
            FreshIOC.Container.Register<IMachineServices>(new MachineAPIService());
            FreshIOC.Container.Register<IStatusServices>(new StausAPIService());
            FreshIOC.Container.Register<IOperatorServices>(new OperatorAPIService());
            FreshIOC.Container.Register<IOrderServices>(new OrderAPIService());
            FreshIOC.Container.Register<IUnitServices>(new UnitAPIService());
            FreshIOC.Container.Register<ILineInspectorServices>(new LineInspecotrAPIService());
            FreshIOC.Container.Register<IAfkeurServices>(new AfkeurAPIService());

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

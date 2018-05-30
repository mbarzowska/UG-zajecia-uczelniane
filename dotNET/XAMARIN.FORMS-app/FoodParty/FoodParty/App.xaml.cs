using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLToolkit.Forms.Controls;
using FoodParty.Data;
using FoodParty.Helpers;
using FoodParty.Repositories;
using FoodParty.ViewModels;
using FoodParty.Views;
using Xamarin.Forms;

namespace FoodParty
{
	public partial class App : Application {
		private static IPizzaSizeRepository _sizesRepository;
		private static IPizzeriaRepository _pizzeriasRepository;
		private static readonly string _databaseName = "super1.db";

		public App ()
		{
			InitializeComponent();
		    FlowListView.Init();
            MainPage = new NavigationPage(new PizzeriasListPage(new PizzeriasGridViewModel()));
		}

		public static IPizzeriaRepository GetPizzeriaRepository {
			get {
				if (_pizzeriasRepository != null) return _pizzeriasRepository;
				var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath(_databaseName);
				var databaseContext = new FPContext(databasePath);
				_pizzeriasRepository = new PizzeriaRepository(databaseContext);
				return _pizzeriasRepository;
			}
		}

		public static IPizzaSizeRepository GetSizeRepository {
			get {
				if (_sizesRepository != null) return _sizesRepository;
				var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath(_databaseName);
				var databaseContext = new FPContext(databasePath);
				_sizesRepository = new PizzaSizeRepository(databaseContext);
				return _sizesRepository;
			}
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

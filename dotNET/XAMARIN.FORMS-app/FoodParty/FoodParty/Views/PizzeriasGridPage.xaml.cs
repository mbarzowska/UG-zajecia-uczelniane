using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodParty.Helpers;
using FoodParty.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodParty.Views {

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PizzeriasListPage : ContentPage {

		public PizzeriasListPage(PizzeriasGridViewModel pizzeriasViewModel) {
			InitializeComponent();
			BindingContext = pizzeriasViewModel;
		}

		protected override void OnAppearing() {
			var viewModel = this.BindingContext as PizzeriasGridViewModel;
			if (viewModel == null)
				return;

			viewModel.RefreshCommand.Execute(null);
		}
	}
}
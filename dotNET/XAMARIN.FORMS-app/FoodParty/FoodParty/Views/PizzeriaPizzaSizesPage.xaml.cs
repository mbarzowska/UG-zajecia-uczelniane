using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodParty.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodParty.Views {

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PizzeriaPizzaSizesPage : ContentPage {

		public PizzeriaPizzaSizesPage (PizzeriaPizzaSizesViewModel viewModel) {
			InitializeComponent();
		    BindingContext = viewModel;
		}

	    protected override void OnAppearing() {
	        var viewModel = this.BindingContext as PizzeriaPizzaSizesViewModel;
	        if (viewModel == null)
	            return;

	        viewModel.RefreshSizes.Execute(null);
	    }
    }
}
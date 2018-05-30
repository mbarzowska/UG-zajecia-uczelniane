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
	public partial class PizzeriaEditDetailsPage : ContentPage {

		public PizzeriaEditDetailsPage (PizzeriaEditDetailsViewModel viewModel) {
            InitializeComponent();
		    BindingContext = viewModel;
		}
    }
}
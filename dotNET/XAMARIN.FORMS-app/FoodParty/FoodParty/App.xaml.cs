using DLToolkit.Forms.Controls;
using FoodParty.ViewModels;
using FoodParty.Views;
using Xamarin.Forms;

namespace FoodParty
{
	public partial class App : Application {
		public App ()
		{
			InitializeComponent();
		    FlowListView.Init();
            MainPage = new NavigationPage(new PizzeriasListPage(new PizzeriasGridViewModel()));
		}
	}
}

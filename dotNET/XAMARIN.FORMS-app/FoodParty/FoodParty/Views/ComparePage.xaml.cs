using FoodParty.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodParty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComparePage : ContentPage
    {
        public ComparePage(CompareViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = this.BindingContext as CompareViewModel;
            if (viewModel == null)
                return;

            viewModel.RefreshPizzerias.Execute(null);
        }
    }
}
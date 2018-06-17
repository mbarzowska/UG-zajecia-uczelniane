using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodParty.Data;
using FoodParty.Helpers;
using FoodParty.Repositories;
using Xamarin.Forms;

namespace FoodParty.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged {
        private const string DatabaseName = "super1.db";

        private static IPizzaSizeRepository _pizzaSizeRepository;

        public IPizzaSizeRepository PizzaSizeRepository {
            get => _pizzaSizeRepository ?? (_pizzaSizeRepository = GetPizzaSizeRepository());
            set => _pizzaSizeRepository = value;
        }

        private static IPizzeriaRepository _pizzeriaRepository;

        public IPizzeriaRepository PizzeriaRepository {
            get => _pizzeriaRepository ?? (_pizzeriaRepository = GetPizzeriaRepository());
            set => _pizzeriaRepository = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static IPizzaSizeRepository GetPizzaSizeRepository() {
            var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath(DatabaseName);
            var databaseContext = new FPContext(databasePath);
            return new PizzaSizeRepository(databaseContext);
        }

        private static IPizzeriaRepository GetPizzeriaRepository() {
            var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath(DatabaseName);
            var databaseContext = new FPContext(databasePath);
            return new PizzeriaRepository(databaseContext);
        }
    }
}

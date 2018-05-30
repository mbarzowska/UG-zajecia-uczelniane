using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FoodParty.Models;
using FoodParty.Validators;
using FoodParty.Views;
using Xamarin.Forms;

namespace FoodParty.ViewModels
{
    public class PizzeriasGridViewModel : BaseViewModel
    {
        private ObservableCollection<Pizzeria> _pizzerias;
        public ObservableCollection<Pizzeria> Pizzerias
        {
            get { return _pizzerias; }
            set
            {
                _pizzerias = value;
                OnPropertyChanged("Pizzerias");
            }
        }

        private string _pizzeriaName;
        public string PizzeriaName
        {
            get { return _pizzeriaName; }
            set
            {
                _pizzeriaName = value;
                OnPropertyChanged("PizzeriaName");
            }
        }

        private string _pizzeriaPhotoURL;
        public string PizzeriaPhotoURL
        {
            get { return _pizzeriaPhotoURL; }
            set
            {
                _pizzeriaPhotoURL = value;
                OnPropertyChanged("PizzeriaPhotoURL");
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var pizzerias = await App.GetPizzeriaRepository.GetPizzeriasAsync();
                    Pizzerias = new ObservableCollection<Pizzeria>(pizzerias);
                });
            }
        }

        public Command AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidatePizzeria(PizzeriaName))
                    {
                        var pizzeria = new Pizzeria
                        {
                            Name = PizzeriaName,
                            PhotoURL = PizzeriaPhotoURL
                        };
                        await App.GetPizzeriaRepository.AddPizzeriaAsync(pizzeria);
                        PizzeriaName = String.Empty;
                        PizzeriaPhotoURL = String.Empty;
                        RefreshCommand.Execute(null);
                        VisibilitySwitchCommand.Execute(null);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Pizzeria name is invalid", "Ok");

                });
            }
        }

        /* GO TO DETAILS */
        private Pizzeria _selectedPizzeria;
        public Pizzeria SelectedPizzeria
        {
            get { return _selectedPizzeria; }
            set
            {
                _selectedPizzeria = value;
                if (_selectedPizzeria == null)
                {
                    return;
                }
                SelectPizzeriaCommand.Execute(_selectedPizzeria);
                _selectedPizzeria = null;
                OnPropertyChanged("SelectedPizzeria");
            }
        }

        public Command SelectPizzeriaCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var pizzeriaDetailsViewModel = new PizzeriaEditDetailsViewModel()
                    {
                        PizzeriaId = SelectedPizzeria.Id,
                        PizzeriaName = SelectedPizzeria.Name,
                        PizzeriaPhotoURL = SelectedPizzeria.PhotoURL,
                        ActualPizzeriaName = SelectedPizzeria.Name,
                        ActualPizzeriaPhotoURL = SelectedPizzeria.PhotoURL
                    };
                    var pizzeriaEditDetailsPage = new PizzeriaEditDetailsPage(pizzeriaDetailsViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(pizzeriaEditDetailsPage);
                });
            }
        }

        public Command GoToCompare
        {
            get
            {
                return new Command(async () =>
                {
                    var compareViewModel = new CompareViewModel();
                    var comparePage = new ComparePage(compareViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(comparePage);
                });
            }
        }

        /* ADD PIZZERIA CONTAINER VISIBILITY */
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public Command VisibilitySwitchCommand
        {
            get { return new Command(() => { IsVisible = !IsVisible; }); }
        }

        /* VIEW MODE */
        private bool _isListMode = true;
        public bool IsListMode {
            get { return _isListMode; }
            set {
                _isListMode = value;
                OnPropertyChanged("IsListMode");
            }
        }

        private bool _isGridMode = false;
        public bool IsGridMode {
            get { return _isGridMode; }
            set {
                _isGridMode = value;
                OnPropertyChanged("IsGridMode");
            }
        }

        public Command SwitchMode {
            get {
                return new Command(() => {
                    IsListMode = !IsListMode;
                    IsGridMode = !IsGridMode;
                });
            }
        }
    }
}

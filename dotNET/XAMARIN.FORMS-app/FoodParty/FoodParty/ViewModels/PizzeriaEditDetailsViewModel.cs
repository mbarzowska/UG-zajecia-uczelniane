using System;
using System.Collections.Generic;
using System.Text;
using FoodParty.Models;
using FoodParty.Validators;
using FoodParty.Views;
using Xamarin.Forms;

namespace FoodParty.ViewModels
{
    public class PizzeriaEditDetailsViewModel : BaseViewModel
    {
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

        private string _pizzeriaName;
        public string PizzeriaName
        {
            get { return _pizzeriaName; }
            set
            {
                if ((value != null && value.Length <= 25) || value == null)
                {
                    _pizzeriaName = value;
                }

                OnPropertyChanged("PizzeriaName");
            }
        }

        private int _pizzeriaId;
        public int PizzeriaId
        {
            get { return _pizzeriaId; }
            set
            {
                _pizzeriaId = value;
                OnPropertyChanged("PizzeriaId");
            }
        }

        private string _actualPizzeriaPhotoURL;
        public string ActualPizzeriaPhotoURL
        {
            get { return _actualPizzeriaPhotoURL; }
            set
            {
                _actualPizzeriaPhotoURL = value;
                OnPropertyChanged("ActualPizzeriaPhotoURL");
            }
        }

        private string _actualPizzeriaName;
        public string ActualPizzeriaName
        {
            get { return _actualPizzeriaName; }
            set
            {
                _actualPizzeriaName = value;
                OnPropertyChanged("ActualPizzeriaName");
            }
        }

        public Command UpdatePizzeria
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidatePizzeria(PizzeriaName))
                    {
                        var pizzeria = await App.GetPizzeriaRepository.GetPizzeriaByIdAsync(PizzeriaId);
                        pizzeria.Name = PizzeriaName;
                        pizzeria.PhotoURL = PizzeriaPhotoURL;
                        var result = await App.GetPizzeriaRepository.UpdatePizzeriaAsync(pizzeria);
                        if (result)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success!", "Pizzeria has been updated!", "Ok");
                            ActualPizzeriaName = PizzeriaName;
                            ActualPizzeriaPhotoURL = PizzeriaPhotoURL;
                        }
                        else 
                        { 
                            await Application.Current.MainPage.DisplayAlert("Error!", "Could not update pizzeria!", "Ok");
                        }
                    }
                    else await Application.Current.MainPage.DisplayAlert("Error!", "Pizzeria name is invalid", "Ok");
                });
            }
        }

        public Command DeletePizzeria
        {
            get
            {
                return new Command(async () =>
                {
                    var answer = await Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure you want to delete this place?", "Yes", "No");
                    if (answer)
                    {
                        await App.GetPizzeriaRepository.RemovePizzeriaAsync(PizzeriaId);
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                });
            }
        }

        public Command GoToPizzaSizes
        {
            get
            {
                return new Command(async () =>
                {
                    var pizzeriaPizzaSizesViewModel = new PizzeriaPizzaSizesViewModel()
                    {
                        PizzeriaId = PizzeriaId,
                        PizzeriaName = ActualPizzeriaName
                    };
                    var pizzeriaPizzaSizesPage = new PizzeriaPizzaSizesPage(pizzeriaPizzaSizesViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(pizzeriaPizzaSizesPage);
                });
            }
        }
    }
}

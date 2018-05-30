using System;
using System.Collections.Generic;
using System.Text;
using FoodParty.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using FoodParty.Validators;

namespace FoodParty.ViewModels
{
    public class PizzeriaPizzaSizesViewModel : BaseViewModel
    {
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

        private ObservableCollection<PizzaSize> _sizes;
        public ObservableCollection<PizzaSize> Sizes
        {
            get { return _sizes; }
            set
            {
                _sizes = value;
                OnPropertyChanged("Sizes");
            }
        }

        private int _size;
        public int Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }

        public PizzaSize _selectedSize;
        public PizzaSize SelectedSize
        {
            get { return _selectedSize; }
            set
            {
                _selectedSize = value;
                if (_selectedSize == null)
                    return;
                DeleteSize.Execute(_selectedSize);
                OnPropertyChanged("SelectedSize");
            }
        }

        public Command AddSize
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidateInt(Size))
                    {
                        var pizzaSize = new PizzaSize()
                        {
                            PizzeriaId = PizzeriaId,
                            Delimeter = Size
                        };

                        await App.GetSizeRepository.AddSizeAsync(pizzaSize);
                        RefreshSizes.Execute(null);
                    }
                    else 
                    { 
                        await Application.Current.MainPage.DisplayAlert("Error!", "Size must be greater then zero!", "Ok");
                    }

                });
            }
        }

        public Command DeleteSize
        {
            get
            {
                return new Command(async () =>
                {
                    var answer = await Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure you want to delete this size?", "Yes", "No");
                    if (answer)
                    {
                        await App.GetSizeRepository.RemoveSizeAsync(_selectedSize.Id);
                        RefreshSizes.Execute(null);
                    }
                });
            }
        }

        public Command RefreshSizes {
            get {
                return new Command(async () => {
                    var sizes = await App.GetSizeRepository.QuerySizesAsync(x => x.PizzeriaId == PizzeriaId);
                    Sizes = new ObservableCollection<PizzaSize>(sizes);
                });
            }
        }
    }
}

using FoodParty.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FoodParty.Validators;

namespace FoodParty.ViewModels
{
    public class CompareViewModel : BaseViewModel
    {
        /* AUTO PIZZERIA MODE */
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

        private Pizzeria _firstPizzeria;
        public Pizzeria FirstPizzeria
        {
            get { return _firstPizzeria; }
            set
            {
                _firstPizzeria = value;
                if (_firstPizzeria == null)
                    return;
                RefreshSizesFromFirstPizzeria.Execute(_firstPizzeria);
                OnPropertyChanged("FirstPizzeria");
            }
        }

        private Pizzeria _secondPizzeria;
        public Pizzeria SecondPizzeria
        {
            get { return _secondPizzeria; }
            set
            {
                _secondPizzeria = value;
                if (_secondPizzeria == null)
                    return;
                RefreshSizesFromSecondPizzeria.Execute(_secondPizzeria);
                OnPropertyChanged("SecondPizzeria");
            }
        }

        private PizzaSize _firstPizzaSize;
        public PizzaSize FirstPizzaSize
        {
            get { return _firstPizzaSize; }
            set
            {
                _firstPizzaSize = value;
                OnPropertyChanged("FirstPizzaSize");
            }
        }

        private PizzaSize _secondPizzaSize;
        public PizzaSize SecondPizzaSize
        {
            get { return _secondPizzaSize; }
            set
            {
                _secondPizzaSize = value;
                OnPropertyChanged("SecondPizzaSize");
            }
        }

        private ObservableCollection<PizzaSize> _pizzaSizesFromFirstPizzeria;
        public ObservableCollection<PizzaSize> PizzaSizesFromFirstPizzeria
        {
            get { return _pizzaSizesFromFirstPizzeria; }
            set
            {
                _pizzaSizesFromFirstPizzeria = value;
                OnPropertyChanged("PizzaSizesFromFirstPizzeria");
            }
        }

        private ObservableCollection<PizzaSize> _pizzaSizesFromSecondPizzeria;
        public ObservableCollection<PizzaSize> PizzaSizesFromSecondPizzeria
        {
            get { return _pizzaSizesFromSecondPizzeria; }
            set
            {
                _pizzaSizesFromSecondPizzeria = value;
                OnPropertyChanged("PizzaSizesFromSecondPizzeria");
            }
        }

        private decimal _firstPrice = 0m;
        public decimal FirstPrice
        {
            get { return _firstPrice; }
            set
            {
                _firstPrice = value;
                OnPropertyChanged("FirstPrice");
            }
        }

        private decimal _secondPrice = 0m;
        public decimal SecondPrice
        {
            get { return _secondPrice; }
            set
            {
                _secondPrice = value;
                OnPropertyChanged("SecondPrice");
            }
        }

        public Command ComparePizzerias
        {
            get
            {
                return new Command(async () =>
                {
                    if (FirstPizzaSize == null || SecondPizzaSize == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Select pizza sizes!", "Ok");
                        return;
                    }
                    if (!ValidateAutoPrices())
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Prices must be greater than zero!", "Ok");
                        return;
                    }
                    var first = FirstPizzaSize.Delimeter / FirstPrice;
                    var second = SecondPizzaSize.Delimeter / SecondPrice;
                    if (first > second)
                        await Application.Current.MainPage.DisplayAlert("Counted!", "The size-to-price ratio is more profitable at " + FirstPizzeria.Name, "Ok");
                    else if (second > first)
                        await Application.Current.MainPage.DisplayAlert("Counted!", "The size-to-price ratio is more profitable at " + SecondPizzeria.Name, "Ok");
                    else
                        await Application.Current.MainPage.DisplayAlert("Counted!", "Both pizzerias have equal size-to-price ratio!", "Ok");
                });
            }
        }

        public Command RefreshPizzerias
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

        public Command RefreshSizesFromFirstPizzeria
        {
            get
            {
                return new Command(async () =>
                {
                    var pizzaSizes = await App.GetSizeRepository.QuerySizesAsync(x => x.PizzeriaId == _firstPizzeria.Id);
                    PizzaSizesFromFirstPizzeria = new ObservableCollection<PizzaSize>(pizzaSizes);
                });
            }
        }

        public Command RefreshSizesFromSecondPizzeria
        {
            get
            {
                return new Command(async () =>
                {
                    var pizzaSizes = await App.GetSizeRepository.QuerySizesAsync(x => x.PizzeriaId == _secondPizzeria.Id);
                    PizzaSizesFromSecondPizzeria = new ObservableCollection<PizzaSize>(pizzaSizes);
                });
            }
        }

        /* CUSTOM MODE */
        private decimal _priceOne;
        public decimal PriceOne
        {
            get { return _priceOne; }
            set
            {
                _priceOne = value;
                OnPropertyChanged("PriceOne");
            }
        }

        private decimal _priceTwo;
        public decimal PriceTwo
        {
            get { return _priceTwo; }
            set
            {
                _priceTwo = value;
                OnPropertyChanged("PriceTwo");
            }
        }

        private decimal _weightOne;
        public decimal WeightOne
        {
            get { return _weightOne; }
            set
            {
                _weightOne = value;
                OnPropertyChanged("WeightOne");
            }
        }

        private decimal _weightTwo;
        public decimal WeightTwo
        {
            get { return _weightTwo; }
            set
            {
                _weightTwo = value;
                OnPropertyChanged("WeightTwo");
            }
        }

        public Command CompareWeightsToPrices
        {
            get
            {
                return new Command(async () =>
                {
                    if (ValidateCustomValues())
                    {
                        var first = WeightOne / PriceOne;
                        var second = WeightTwo / PriceTwo;
                        if (first > second)
                            await Application.Current.MainPage.DisplayAlert("Counted!", "First size-to-price ratio is more profitable!", "Ok");
                        else if (second > first)
                            await Application.Current.MainPage.DisplayAlert("Counted!", "Second size-to-price ratio is more profitable!", "Ok");
                        else
                            await Application.Current.MainPage.DisplayAlert("Counted!", "Both size-to-price ratios are equal!", "Ok");


                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Both weights and prices must be greater then zero and have correct format!", "Ok");
                });
            }
        }

        /* VALIDATION HELP */
        private bool ValidateCustomValues()
        {
            return (Validator.ValidatePrice(PriceOne) && Validator.ValidatePrice(PriceTwo)
                && Validator.ValidateDecimal(WeightOne) && Validator.ValidateDecimal(WeightTwo));
        }

        private bool ValidateAutoPrices()
        {
            return (Validator.ValidatePrice(FirstPrice) && Validator.ValidatePrice(SecondPrice));
        }

        /* SWITCH */
        private bool _isAutoMode = true;
        public bool IsAutoMode {
            get { return _isAutoMode; }
            set {
                _isAutoMode = value;
                OnPropertyChanged("IsAutoMode");
            }
        }

        private bool _isManualMode = false;
        public bool IsManualMode {
            get { return _isManualMode; }
            set {
                _isManualMode = value;
                OnPropertyChanged("IsManualMode");
            }
        }

        public Command SwitchMode {
            get {
                return new Command(() => {
                    IsAutoMode = !IsAutoMode;
                    IsManualMode = !IsManualMode;
                });
            }
        }
    }
}
